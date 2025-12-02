using GMutagenEngine.Infrastructure.Builders.Common;
using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;

namespace GMutagenEngine.Infrastructure.Builders.Examples.Sync
{
    [HandlerFor(typeof(IParametrizedBuilderStepOne), nameof(IParametrizedBuilderStepOne.Initialize))]
    public class StepOneHandler : ISyncActionHandler
    {
        public void Handle()
        {
            Console.WriteLine("Step One: Initialize()");
        }
    }
}