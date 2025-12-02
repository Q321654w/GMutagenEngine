using GMutagenEngine.Infrastructure.Builders.Async.Common;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Builders.Async
{
    public class ContextualBuilder<TBuilder, TContext>(IBuilderIMediator mediator, TContext context) : IBuilder
        where TBuilder : ContextualBuilder<TBuilder, TContext>
    {
        public async Task<IBuilder> Execute(IId id)
        {
            await mediator.Send(context, id);
            return this;
        }
    }
}