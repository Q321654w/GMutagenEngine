using GMutagenEngine.Infrastructure.Builders.Async.Common;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Builders.Async
{
    public class ParametrizedBuilder<TBuilder>(IBuilderIMediator mediator) : IParametrizedBuilder
        where TBuilder : ParametrizedBuilder<TBuilder>
    {
        public async Task<IBuilder> Execute(IId id)
        {
            await mediator.Send(id);
            return this;
        }

        public async Task<IParametrizedBuilder> Execute<TData>(TData data, IId id)
        {
            await mediator.Send(data, id);
            return this;
        }
    }
}