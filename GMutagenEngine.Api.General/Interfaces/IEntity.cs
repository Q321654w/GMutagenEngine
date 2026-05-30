using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Api.General.Interfaces;

public interface IEntity : IEntityMark {
}

public interface IEntity<TId> : IEntity, IIdentifiable<TId>, IEntityMark {
}
public interface IEntityMark : ISelfMark<IEntityMark> {
}

