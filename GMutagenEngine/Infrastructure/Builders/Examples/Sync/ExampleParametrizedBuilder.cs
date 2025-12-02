using GMutagenEngine.Infrastructure.Builders.Sync;
using GMutagenEngine.Infrastructure.Builders.Sync.Common;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    public class ExampleParametrizedBuilder(IBuilderIMediator mediator)
        : ParametrizedBuilder<ExampleParametrizedBuilder>(mediator),
            IParametrizedBuilderStepOne,
            IParametrizedBuilderStepTwo,
            IParametrizedBuilderStepThree;
}