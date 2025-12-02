namespace GMutagenEngine.Infrastructure.Handlers.Sync.Actions
{
    public interface ISyncActionHandler
    {
        void Handle();
    }

    public interface ISyncActionHandler<in TIn>
    {
        void Handle(TIn data);
    }


 
}