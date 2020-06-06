using EncryptedLegder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptedLegder.Processes
{
    internal class LedgerVerification
    {
        private readonly string salt;
        public LedgerVerification(string salt)
        {
            this.salt = salt;
        }
        public bool VerifyEntry(EncryptedLedgerEntry encryptedLedgerEntry)
        {

        }
    }
}
