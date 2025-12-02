namespace GMutagenEngine.Infrastructure.Builders.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class HandlerForAttribute(Type interfaceType, string methodName) : Attribute
    {
        public Type InterfaceType { get; } = interfaceType;
        public string MethodName { get; } = methodName;
    }
}