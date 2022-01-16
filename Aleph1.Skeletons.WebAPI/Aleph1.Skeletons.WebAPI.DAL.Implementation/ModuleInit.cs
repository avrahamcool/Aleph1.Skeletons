using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

using Aleph1.DI.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	/// <summary>Used to register concrete implementations to the DI container</summary>
	[CLSCompliant(false), Export(typeof(IModule))]
	public class ModuleInit : IModule
	{
		/// <summary>Used to register concrete implementations to the DI container</summary>
		/// <param name="registrar">add implementation to the DI container using this registrar</param>
		public void Initialize(IModuleRegistrar registrar)
		{
			Contract.Requires(registrar != null);

			registrar.RegisterTypeAsSingelton<DbContextOptions<GenericContext>, DbContextOptions<GenericContext>>(SettingsManager.DBOptions);
			registrar.RegisterType<GenericContext, GenericContext>();
			registrar.RegisterType<IGenericRepo, GenericRepo>();
		}
	}
}
