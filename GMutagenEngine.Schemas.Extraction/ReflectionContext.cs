using System.Reflection;

namespace GMutagenEngine.Schemas.Extraction;

public class ReflectionContext(Type declaredType, Type actualType, object? targetInstance, MemberInfo? memberInfo, ReflectionContext? parentContext = null)
{
    public ReflectionContext? ParentContext { get; } = parentContext;
    public Type DeclaredType { get; } = declaredType;
    public Type ActualType { get; } = actualType;
    public object? TargetInstance { get; } = targetInstance;
    public MemberInfo? MemberInfo { get; } = memberInfo;
}