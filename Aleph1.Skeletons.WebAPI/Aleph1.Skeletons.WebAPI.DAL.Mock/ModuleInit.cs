using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

using Aleph1.DI.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
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
			registrar.RegisterTypeAsSingelton<DbContextOptions<GenericContextMock>, DbContextOptions<GenericContextMock>>(SettingsManager.DBOptions);
			registrar.RegisterType<GenericContextMock, GenericContextMock>();
			registrar.RegisterType<IGenericRepo, GenericRepoMock>();
			GenericSeeder.SeedAll();
		}
	}
}
