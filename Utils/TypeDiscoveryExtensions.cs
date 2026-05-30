namespace Utils;

public static class TypeDiscoveryExtensions
{
    public static TypeDiscovery AsTypeDiscovery(this Type type)
        => new(type, new TypeDiscoverySettings());

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        Func<TypeDiscoverySettings, TypeDiscoverySettings> configure)
        => new(type, configure(new TypeDiscoverySettings()));

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        Action<TypeDiscoverySettings> configure)
    {
        var settings = new TypeDiscoverySettings();
        configure(settings);
        return new TypeDiscovery(type, settings);
    }

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        TypeDiscoverySettings settings)
        => new(type, settings);
}