using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptedLegder.Injections
{
    public interface IInjection
    {
        IContainer Inject();
    }
}
