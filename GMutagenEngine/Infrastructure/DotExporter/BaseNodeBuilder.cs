namespace GMutagenEngine.Infrastructure.DotExporter
{
    public abstract class BaseNodeBuilder<T>(string id)
        where T : BaseNodeBuilder<T>
    {
        protected readonly DotNode Node = new(id);

        public T Label(string label) => Set(DotAttribute.Label, label);
        public T Shape(DotShape shape) => Set(DotAttribute.Shape, shape.ToAttributeKey());
        public T Color(DotColor color) => Set(DotAttribute.Color, color.ToAttributeKey());
        public T Style(DotStyle style) => Set(DotAttribute.Style, style.ToAttributeKey());

        public T Set(DotAttribute attr, string value)
        {
            Node.Set(attr, value);
            return (T)this;
        }
    }
}