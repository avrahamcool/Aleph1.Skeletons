using Aleph1.DI.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using System.ComponentModel.Composition;

namespace Aleph1.Skeletons.WebAPI.DAL.Moq
{
    /// <summary>Used to register concrete implemtations to the DI container</summary>
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
		/// <summary>Used to register concrete implemtations to the DI container</summary>
		/// <param name="registrar">add implementation to the DI container using this registrar</param>
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterTypeAsSingelton<IDAL, DALMoq>();
        }
    }
}
