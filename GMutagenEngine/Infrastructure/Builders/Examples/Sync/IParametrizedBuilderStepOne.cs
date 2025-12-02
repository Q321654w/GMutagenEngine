using GMutagenEngine.Infrastructure.Builders.Sync;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    public interface IParametrizedBuilderStepOne : IParametrizedBuilder
    {
        IParametrizedBuilderStepTwo Initialize() 
            => (IParametrizedBuilderStepTwo)Execute(NameOf<IParametrizedBuilderStepOne>());
    }
}