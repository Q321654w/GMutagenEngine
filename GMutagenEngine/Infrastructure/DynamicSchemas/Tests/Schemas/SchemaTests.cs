using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Schemas;

public class SchemaTests
{
    [Fact]
    public void AddMember_NewMember_AddsMember()
    {
        var schema = new Schema();
        var memberInfo = new SchemaMemberInfo { Name = "Test" };

        schema.AddMember(memberInfo);

        Assert.Single(schema.Members);
        Assert.Same(memberInfo, schema.Members["Test"]);
    }

    [Fact]
    public void AddMember_DuplicateName_ReplacesMember()
    {
        var schema = new Schema();
        var member1 = new SchemaMemberInfo { Name = "Test" };
        var member2 = new SchemaMemberInfo { Name = "Test" };

        schema.AddMember(member1);
        schema.AddMember(member2);

        Assert.Single(schema.Members);
        Assert.Same(member2, schema.Members["Test"]);
    }
}