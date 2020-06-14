using Autofac;

namespace EncryptedLegder.Injections
{
    public interface IInjection
    {
        IContainer Inject();
        void RegisterInjections();
    }
}
