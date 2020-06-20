using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Linq;
using System.Text;

namespace EncryptedLegder.Internal
{
    internal class EncryptionDecryption : ICryptography
    {
        private readonly string key;
        private readonly Encoding encoding;
        private readonly IBlockCipherPadding padding;
        private readonly IDigitalSigning digitalSigning;

        public EncryptionDecryption(IEncryptionKey encryptionKey, IDigitalSigning digitalSigning)
        {
            key = encryptionKey.GetEncryptionKey();
            encoding = Encoding.ASCII;
            padding = new Pkcs7Padding();
            this.digitalSigning = digitalSigning;
        }
        public string Encrypt(string plainValue)
        {
            var encryptedString = GetEngine().Encrypt(plainValue, key);
            return encryptedString;
        }
        public string Decrypt(string cipherValue)
        {
            var decryptedString = GetEngine().Decrypt(cipherValue, key);
            return decryptedString;
        }
        private BCEngine GetEngine()
        {
            BCEngine bCEngine = new BCEngine(new AesEngine(), encoding);
            bCEngine.SetPadding(padding);
            return bCEngine;
        }

        public EncryptedLedgerEntry<TrsanctioneeIdType> Encrypt<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry)
        {
            EncryptedLedgerEntry<TrsanctioneeIdType> encrypted = new EncryptedLedgerEntry<TrsanctioneeIdType>();
            var type = typeof(LedgerEntry<TrsanctioneeIdType>);
            foreach (var property in type.GetProperties().Where(x => x.Name != "TransactioneeId"))
            {
                if (property.Name != "PrimaryKey")
                {
                    var cipher = Encrypt(property.GetValue(ledgerEntry).ToString());
                    var name = property.Name;
                    typeof(EncryptedLedgerEntry<TrsanctioneeIdType>).GetProperties()
                                                .Where(x => x.Name == name)
                                                .FirstOrDefault()
                                                .SetValue(encrypted, cipher);
                }
                else
                {
                    encrypted.PrimaryKey = ledgerEntry.PrimaryKey;
                }
            }
            encrypted.TransactioneeId = ledgerEntry.TransactioneeId;
            encrypted.Signature = digitalSigning.GetSignature(ledgerEntry);
            return encrypted;
        }

        public LedgerEntry<TrsanctioneeIdType> Decrypt<TrsanctioneeIdType>(EncryptedLedgerEntry<TrsanctioneeIdType> ledgerEntry, out bool verificationFlag)
        {
            LedgerEntry<TrsanctioneeIdType> decrypt = new LedgerEntry<TrsanctioneeIdType>();
            var type = typeof(EncryptedLedgerEntry<TrsanctioneeIdType>);
            string signature = "";
            foreach (var property in type.GetProperties().Where(x => x.Name != "TransactioneeId"))
            {
                if (property.Name == "Signature")
                {
                    signature = property.GetValue(ledgerEntry).ToString();
                }
                else if (property.Name != "PrimaryKey")
                {
                    var value = Decrypt(property.GetValue(ledgerEntry).ToString());
                    var name = property.Name;
                    foreach (var item in typeof(LedgerEntry<TrsanctioneeIdType>).GetProperties())
                    {
                        if (item.Name == name)
                        {
                            item.SetValue(decrypt, Convert.ChangeType(value, item.PropertyType));
                            break;
                        }
                    }
                }
                else
                {
                    decrypt.PrimaryKey = Convert.ToInt64(ledgerEntry.PrimaryKey);
                }
            }
            decrypt.TransactioneeId = ledgerEntry.TransactioneeId;
            verificationFlag = digitalSigning.VerifySignature(decrypt, signature);
            return decrypt;
        }
    }
    internal class BCEngine
    {
        private readonly Encoding _encoding;
        private readonly IBlockCipher _blockCipher;
        private PaddedBufferedBlockCipher _cipher;
        private IBlockCipherPadding _padding;

        public BCEngine(IBlockCipher blockCipher, Encoding encoding)
        {
            _blockCipher = blockCipher;
            _encoding = encoding;
        }

        public void SetPadding(IBlockCipherPadding padding)
        {
            if (padding != null)
                _padding = padding;
        }

        public string Encrypt(string plain, string key)
        {
            byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(plain), key);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipher, string key)
        {
            byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), key);
            return _encoding.GetString(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="forEncrypt"></param>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="CryptoException"></exception>


        private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
        {
            try
            {
                _cipher = _padding == null ? new PaddedBufferedBlockCipher(_blockCipher) : new PaddedBufferedBlockCipher(_blockCipher, _padding);
                byte[] keyByte = _encoding.GetBytes(key);
                _cipher.Init(forEncrypt, new KeyParameter(keyByte));
                return _cipher.DoFinal(input);
            }
            catch (CryptoException ex)
            {
                throw ex;
            }
        }
    }
}
