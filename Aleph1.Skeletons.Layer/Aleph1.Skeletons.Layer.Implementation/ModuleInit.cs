using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

using Aleph1.DI.Contracts;
using Aleph1.Skeletons.Layer.Contracts;

namespace Aleph1.Skeletons.Layer.Implementation
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

			//You can register as many types as you want into the Container

			//registrar.RegisterType<ITest, Test>();
			//registrar.RegisterTypeAsSingelton<ITest, Test>();

			registrar.RegisterType<ILayer, Layer>();
		}
	}
}
