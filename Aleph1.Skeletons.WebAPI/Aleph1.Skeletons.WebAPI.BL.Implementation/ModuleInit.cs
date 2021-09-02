using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

using Aleph1.DI.Contracts;
using Aleph1.Skeletons.WebAPI.BL.Contracts;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
{
	/// <summary>Module initialization</summary>
	[CLSCompliant(false), Export(typeof(IModule))]
	public class ModuleInit : IModule
	{
		/// <summary>Register concrete implementations to the dependency injection container</summary>
		/// <param name="registrar">Add an implementation using this registrar</param>
		public void Initialize(IModuleRegistrar registrar)
		{
			Contract.Requires(registrar != null);
			registrar.RegisterType<IPersonBL, PersonBL>();
		}
	}
}
