namespace GMutagenEngine.Infrastructure.Builders.Common
{
    public static class MethodId<TInterface>
    {
        public static string For(string name)
        {
            return $"{typeof(TInterface).Name}.{name}";
        }
    }
}