using GMutagenEngine.Identification.Tagging.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Configuration;

public record AssemblyDefinition<TAssemblyId>(
  TAssemblyId Id,
  HashSet<ITag> Tags,
  string Name,
  string Version,
  string? Culture
) : IConfig<TAssemblyId>;

public record TypeDefinition<TTypeId, TAssemblyId>(
  TTypeId Id,
  HashSet<ITag> Tags,
  TAssemblyId AssemblyId,
  string Namespace,
  string Name
) : IConfig<TTypeId>;

public record SchemaDefinition<TSchemaId, TTypeId, TPropertyId>(
  TSchemaId Id,
  HashSet<ITag> Tags,
  TTypeId TypeId,
  Dictionary<TPropertyId, TSchemaId> Properties
) : IConfig<TSchemaId>
  where TPropertyId : notnull;

public record ValueDefinition<TValueId, TSchemaId>(
  TValueId Id,
  HashSet<ITag> Tags,
  TSchemaId SchemaId
) : IConfig<TValueId>;

public record ServiceDefinition<TServiceId, TSchemaId>(
  TServiceId Id,
  HashSet<ITag> Tags,
  TSchemaId SchemaId
) : IConfig<TServiceId>;

public record StructureDefinition<TModuleId>(
  TModuleId Id,
  HashSet<ITag> Tags,
  List<TModuleId> ModuleIdes
) : IConfig<TModuleId>;

public record ModuleDefinition<TId, TConfigId>(
  TId Id,
  HashSet<ITag> Tags,
  List<TConfigId> ConfigIdes
) : IConfig<TId>;





public interface IConfig<T> : IConfigMark {
}
public interface IConfigMark : ISelfMark<IConfigMark> {
}
