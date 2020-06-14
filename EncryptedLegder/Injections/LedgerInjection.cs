using Autofac;
using EncryptedLegder.Abstractions;
using EncryptedLegder.Internal;
using EncryptedLegder.Processes;

namespace EncryptedLegder.Injections
{
    public abstract class LedgerInjection<TransactioneeIdType> : IInjection
    {
        private readonly string salt;
        private readonly string encryptionKey;
        protected readonly ContainerBuilder builder;

        protected LedgerInjection(string salt, string encryptionKey)
        {
            this.salt = salt;
            this.encryptionKey = encryptionKey;
            builder = new ContainerBuilder();
        }
        public IContainer Inject()
        {
            //AutoFac Registeration
            RegisterInjections();
            return builder.Build();
        }

        public virtual void RegisterInjections()
        {
            builder.Register(x => new SaltValue(salt))
                   .As<ISaltValue>();
            builder.Register(x => new EncryptionKey(encryptionKey))
                   .As<IEncryptionKey>();
            builder.RegisterType<EncryptionDecryption>()
                   .As<ICryptography>();
            builder.RegisterType<DigitalSigning>()
                   .As<IDigitalSigning>();
            builder.RegisterType<LedgerTransaction<TransactioneeIdType>>()
                   .As<ILedgerTransaction<TransactioneeIdType>>();
            builder.RegisterType<LedgerVerification<TransactioneeIdType>>()
                   .As<ILedgerVerification<TransactioneeIdType>>();
        }
        public void RegisterLedgerCRUD<ConcreteClass>()
        {
            builder.RegisterType<ConcreteClass>().As<ILedgerCrud<TransactioneeIdType>>();
        }
    }
}
