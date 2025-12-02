using GMutagenEngine.Infrastructure.Builders.Sync;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    public interface IParametrizedBuilderStepThree : IParametrizedBuilder
    {
        IParametrizedBuilderStepOne Complete()
            => (IParametrizedBuilderStepOne)Execute(NameOf<IParametrizedBuilderStepOne>());
    }
}