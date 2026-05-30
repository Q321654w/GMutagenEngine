namespace GMutagenEngine.MetaData.Runtime.Marks;

public interface ISelfMark<T> : IMark<T> where T : ISelfMark<T> {
    
}