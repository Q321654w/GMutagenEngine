using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class SchemaMemberInfoTests
{
    [Fact]
    public void GetMemberValue_Property_ReturnsValue()
    {
        var model = new SimpleModel { Id = 42 };
        var memberInfo = new SchemaMemberInfo
        {
            Info = typeof(SimpleModel).GetProperty("Id")
        };

        var value = memberInfo.GetMemberValue(model);

        Assert.Equal(42, value);
    }

    [Fact]
    public void GetMemberValue_Field_ReturnsValue()
    {
        var model = new ModelWithFields { PublicField = 123 };
        var memberInfo = new SchemaMemberInfo
        {
            Info = typeof(ModelWithFields).GetField("PublicField")
        };

        var value = memberInfo.GetMemberValue(model);

        Assert.Equal(123, value);
    }

    [Fact]
    public void GetMemberValue_WriteOnlyProperty_ReturnsNull()
    {
        var memberInfo = new SchemaMemberInfo
        {
            Info = null
        };

        var value = memberInfo.GetMemberValue(new object());

        Assert.Null(value);
    }
}