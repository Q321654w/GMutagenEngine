using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.General.Topics;

public interface ITopic : ITopicMark
{
}

public interface ITopic<TId> : ITopic, IIdentifiable<TId>
{
}


public interface ISingleTopic : ITopic, ISingleTopicMark
{
}

public interface ISingleTopic<TId> : ITopic<TId>, ISingleTopic
{
}



public interface IFanOutTopic : ITopic, IFanOutTopicMark
{
}

public interface IFanOutTopic<TId> : ITopic<TId>, IFanOutTopic
{
}


public interface ITopicMark : ISelfMark<ITopicMark>
{
}

public interface ISingleTopicMark : ISelfMark<ISingleTopicMark>
{
}

public interface IFanOutTopicMark : ISelfMark<IFanOutTopicMark>
{
}




