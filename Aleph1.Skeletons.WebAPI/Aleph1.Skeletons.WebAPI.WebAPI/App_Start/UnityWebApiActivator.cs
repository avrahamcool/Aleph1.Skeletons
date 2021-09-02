using System;
using System.Reflection;
using System.Web;
using System.Web.Http;

using Aleph1.DI.Contracts;
using Aleph1.DI.UnityImplementation;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;

using FluentValidation;
using FluentValidation.WebApi;

using Unity;
using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebApiActivator), nameof(UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebApiActivator), nameof(UnityWebApiActivator.Shutdown))]

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
	/// <summary>Integrate Unity with WebAPI</summary>
	internal static class UnityWebApiActivator
	{
		private static IUnityContainer DIContainer { get; set; }

		/// <summary>Integrates Unity when the application starts</summary>
		public static void Start()
		{
			// Create an empty container
			DIContainer = new UnityContainer();

			// Register all components
			ModuleLoader.LoadModulesFromAssemblies(new UnityModuleRegistrar(DIContainer), AppDomain.CurrentDomain.BaseDirectory, SettingsManager.BaseModulesDir, SettingsManager.ModulesPath);

			// Configure FluentValidation, with Unity as resolver
			FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration, c => c.ValidatorFactory = new UnityValidatorFactory(DIContainer));

			// Register all public Validators from this assembly
			AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
				.ForEach(validator => DIContainer.RegisterType(validator.InterfaceType, validator.ValidatorType));

			DIContainer.RegisterFactory<Identity>(container =>
			{
				ISecurity security = container.Resolve<ISecurity>();
				return HttpContext.Current.Request.GetClaimsFromCookies(security);
			});

			// Point the WebAPI to use the container
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(DIContainer);
		}

		/// <summary>Dispose Unity container when the application shuts down</summary>
		public static void Shutdown() => DIContainer.Dispose();
	}
}
