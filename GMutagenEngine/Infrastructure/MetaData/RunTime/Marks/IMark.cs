namespace GMutagenEngine.Infrastructure.MetaData.RunTime.Marks;

public interface IMark
{
    
}

public interface IMark<T> : IMark
{
    
}

public interface ISelfMark<T> : IMark
    where T : ISelfMark<T>
{
    
}

