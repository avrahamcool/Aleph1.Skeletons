using Aleph1.DI.Contracts;
using Aleph1.DI.UnityImplementation;
using Aleph1.Skeletons.WebAPI.WebAPI;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;

using FluentValidation;
using FluentValidation.WebApi;

using System;
using System.Reflection;
using System.Web.Http;

using Unity;
using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebApiActivator), nameof(UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebApiActivator), nameof(UnityWebApiActivator.Shutdown))]

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.</summary>
    internal static class UnityWebApiActivator
    {
        private static IUnityContainer DIContainer { get; set; }

        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            // create an empty container
            DIContainer = new UnityContainer();

            // register all your components with the container here
            ModuleLoader.LoadModulesFromAssemblies(new UnityModuleRegistrar(DIContainer), AppDomain.CurrentDomain.BaseDirectory, SettingsManager.ModulesPath);

            //Configure Model Validation to use FluentValidation, with Unity as resolver
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration, c => c.ValidatorFactory = new UnityValidatorFactory(DIContainer));

            //Register all public Validators from this assembly
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(validator => DIContainer.RegisterType(validator.InterfaceType, validator.ValidatorType));

            // point the WebAPI to use the container
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(DIContainer);
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            DIContainer.Dispose();
        }
    }
}
