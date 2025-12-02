using GMutagenEngine.Infrastructure.Builders.Sync.Common;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Builders.Sync
{
    public class ContextualBuilder<TBuilder, TContext>(IBuilderIMediator mediator, TContext context) : IBuilder
        where TBuilder : ContextualBuilder<TBuilder, TContext>
    {
        public IBuilder Execute(IId id)
        {
            mediator.Send(context, id);
            return this;
        }
    }
}