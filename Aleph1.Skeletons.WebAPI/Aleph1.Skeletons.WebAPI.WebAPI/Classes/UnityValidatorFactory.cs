using FluentValidation;
using System;
using Unity;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Classes
{
    class UnityValidatorFactory : ValidatorFactoryBase
    {
        private readonly IUnityContainer container;

        public UnityValidatorFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (container.IsRegistered(validatorType))
                return container.Resolve(validatorType) as IValidator;
            return null;
        }
    }
}