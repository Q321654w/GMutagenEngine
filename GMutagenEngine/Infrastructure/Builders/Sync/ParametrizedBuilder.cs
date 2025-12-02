using GMutagenEngine.Infrastructure.Builders.Sync.Common;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Builders.Sync
{
    public class ParametrizedBuilder<TBuilder>(IBuilderIMediator mediator) : IParametrizedBuilder
        where TBuilder : ParametrizedBuilder<TBuilder>
    {
        public IBuilder Execute(IId id)
        {
            mediator.Send(id);
            return this;
        }

        public IParametrizedBuilder Execute<TData>(TData data, IId id)
        {
            mediator.Send(data, id);
            return this;
        }
    }
}