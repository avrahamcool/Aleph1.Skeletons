using Aleph1.DI.Contracts;
using Aleph1.Skeletons.WebAPI.BL.Contracts;

using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
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

            registrar.RegisterType<IBL, BL>();
        }
    }
}
