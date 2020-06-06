using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using EncryptedLegder.Abstractions;
using EncryptedLegder.Internal;
using EncryptedLegder.Processes;

namespace EncryptedLegder.Injections
{
    public class LedgerInjection<TransactioneeIdType> : IInjection
    {
        private readonly string salt;
        private readonly string encryptionKey;

        public LedgerInjection(string salt, string encryptionKey)
        {
            this.salt = salt;
            this.encryptionKey = encryptionKey;
        }
        public IContainer Inject()
        {
            //AutoFac Registeration
            var builder = new ContainerBuilder();
            builder.Register(x => new SaltValue(salt)).As<ISaltValue>();
            builder.Register(x => new EncryptionKey(encryptionKey)).As<IEncryptionKey>();
            builder.RegisterType<EncryptionDecryption>().As<ICryptography>();
            builder.RegisterType<LedgerTransaction<TransactioneeIdType>>().As<ILedgerTransaction<TransactioneeIdType>>();
            return builder.Build();
        }
    }
}
