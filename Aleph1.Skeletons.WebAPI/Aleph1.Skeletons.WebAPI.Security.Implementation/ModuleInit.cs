using Aleph1.DI.Contracts;
using Aleph1.Security.Contracts;
using Aleph1.Security.Implementation.RijndaelManagedCipher;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

using System.ComponentModel.Composition;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    /// <summary>Used to register concrete implementations to the DI container</summary>
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        /// <summary>Used to register concrete implementations to the DI container</summary>
        /// <param name="registrar">add implementation to the DI container using this registrar</param>
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterTypeAsSingelton<ICipher, RijndaelManagedCipher>();
            registrar.RegisterTypeAsSingelton<ISecurity, SecurityService>();
        }
    }
}
