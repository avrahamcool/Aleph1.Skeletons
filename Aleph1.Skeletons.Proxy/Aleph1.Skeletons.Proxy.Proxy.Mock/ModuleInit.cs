using Aleph1.DI.Contracts;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;

using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

namespace Aleph1.Skeletons.Proxy.Proxy.Mock
{
    /// <summary>Used to register concrete implementations to the DI container</summary>
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        /// <summary>Used to register concrete implementations to the DI container</summary>
        /// <param name="registrar">add implementation to the DI container using this registrar</param>
        public void Initialize(IModuleRegistrar registrar)
        {
            Contract.Requires(registrar != null);

            registrar.RegisterTypeAsSingelton<IProxy, ProxyMock>();
        }
    }
}
