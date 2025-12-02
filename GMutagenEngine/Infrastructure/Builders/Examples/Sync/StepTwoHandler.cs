using GMutagenEngine.Infrastructure.Builders.Common;
using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    [HandlerFor(typeof(IParametrizedBuilderStepTwo), nameof(IParametrizedBuilderStepTwo.Configure))]
    public class StepTwoHandler : ISyncActionHandler<string>
    {
        public void Handle(string data)
        {
            Console.WriteLine($"Step Two: Configure('{data}')");
        }
    }
}