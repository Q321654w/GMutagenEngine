using GMutagenEngine.Infrastructure.Builders.Sync;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    public interface IParametrizedBuilderStepTwo : IParametrizedBuilder
    {
        IParametrizedBuilderStepThree Configure(string configName)
            => (IParametrizedBuilderStepThree)Execute(configName, NameOf<IParametrizedBuilderStepOne>());
    }
}