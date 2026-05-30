namespace GMutagenEngine.Mediators.General;

public class MiddlewareIdFactory : IMiddlewareIdFactory<HandlerId, TypePairId>
{
    public TypePairId Create(HandlerId? id)
    {
        return Create(typeof(object), typeof(object)); 
    }

    public TypePairId CreateIn<TIn>(TIn data, HandlerId? id)
    {
        return Create(typeof(TIn), typeof(object)); 
    }

    public TypePairId CreateOut<TOut>(HandlerId? id)
    {
        return Create(typeof(object), typeof(TOut)); 
    }

    public TypePairId CreateInOut<TIn, TOut>(TIn data, HandlerId? id)
    {
        return Create(typeof(TIn), typeof(TOut)); 
    }

    private TypePairId Create(Type inType, Type outType)
    {
        return new TypePairId(inType, outType);
    }
}