using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations;
using Xunit;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class RecursiveStructureTests
{
    private readonly ISchemaRegistry _schemaRegistry = new SchemaRegistry();
    private readonly IContextRegistry _contextRegistry = new ContextRegistry();

    [Fact]
    public void Generate_RecursiveStructure_LiveContext_HandlesCyclicReferences()
    {
        // Arrange
        var a = new A();
        var b = new B();
        a.B = b;
        b.A = a; // создаем циклическую ссылку

        // Act
        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        Assert.NotNull(contextA);
        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);
        var contextAFromB = contextB.GetContext("A");
        Assert.NotNull(contextAFromB);

        // Убедимся, что это тот же самый объект A (по ссылке)
        Assert.Same(contextA, contextAFromB);
    }

    [Fact]
    public void Generate_RecursiveStructure_SnapshotContext_HandlesCyclicReferences()
    {
        // Arrange
        var a = new A();
        var b = new B();
        a.B = b;
        b.A = a;

        // Act
        var contextA = a.ToSnapshotContext(_schemaRegistry);

        // Assert
        Assert.NotNull(contextA);
        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);
        var contextAFromB = contextB.GetContext("A");
        // В снапшоте мы не ожидаем ту же самую инстанцию, но структура должна быть сохранена
        Assert.NotNull(contextAFromB);
        // Проверяем, что мы можем получить значения из контекста A, который вложен в B
        // Например, если бы в A было поле, мы могли бы его проверить, но в данном случае полей нет.
        // Поэтому просто проверяем, что контекст не null и структура создана.
    }

    [Fact]
    public void Generate_RecursiveStructure_WithValues_LiveContext_ReturnsCorrectValues()
    {
        var a = new AWithData { DataA = "DataA" };
        var b = new BWithData { DataB = "DataB" };
        a.B = b;
        b.A = a;

        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        Assert.NotNull(contextA);
        Assert.Equal("DataA", contextA.GetValue("DataA").Value);

        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);
        Assert.Equal("DataB", contextB.GetValue("DataB").Value);

        var contextAFromB = contextB.GetContext("A");
        Assert.NotNull(contextAFromB);
        Assert.Equal("DataA", contextAFromB.GetValue("DataA").Value);
    }

    [Fact]
    public void Generate_RecursiveStructure_WithValues_SnapshotContext_ReturnsCorrectValues()
    {
        var a = new AWithData { DataA = "DataA" };
        var b = new BWithData { DataB = "DataB" };
        a.B = b;
        b.A = a;

        var contextA = a.ToSnapshotContext(_schemaRegistry);

        Assert.NotNull(contextA);
        Assert.Equal("DataA", contextA.GetValue("DataA").Value);

        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);
        Assert.Equal("DataB", contextB.GetValue("DataB").Value);

        var contextAFromB = contextB.GetContext("A");
        Assert.NotNull(contextAFromB);
        Assert.Equal("DataA", contextAFromB.GetValue("DataA").Value);
    }

    [Fact]
    public void Generate_DeepRecursivePath_LiveContext_WorksCorrectly()
    {
        // Arrange
        var a = new A();
        var b = new B();
        var a2 = new A();
        var b2 = new B();

        a.B = b;
        b.A = a2;
        a2.B = b2;
        // b2.A остается null

        // Act
        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        Assert.NotNull(contextA);

        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);

        var contextA2 = contextB.GetContext("A");
        Assert.NotNull(contextA2);

        var contextB2 = contextA2.GetContext("B");
        Assert.NotNull(contextB2);

        var contextA3 = contextB2.GetContext("A"); // Должен быть null, так как b2.A = null
        Assert.Null(contextA3);
    }

    [Fact]
    public void Generate_PartialRecursiveStructure_LiveContext_HandlesNullReferences()
    {
        // Arrange
        var a = new A();
        a.B = null; // B не установлен

        // Act
        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert
        Assert.NotNull(contextA);
        var contextB = contextA.GetContext("B");
        Assert.Null(contextB); // B должен быть null
    }

    [Fact]
    public void Generate_RecursiveStructure_GetAllContexts_DoesNotStackOverflow()
    {
        // Arrange
        var a = new A();
        var b = new B();
        a.B = b;
        b.A = a;

        // Act
        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert - этот вызов не должен приводить к StackOverflowException
        var allContexts = contextA.GetAllContexts().ToList();

        // Должны быть как минимум контексты для A и B
        Assert.True(allContexts.Count >= 2);
    }

    [Fact]
    public void Generate_RecursiveStructure_GetAllValues_DoesNotStackOverflow()
    {
        // Arrange
        var a = new A();
        var b = new B();
        a.B = b;
        b.A = a;

        // Act
        var contextA = a.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert - этот вызов не должен приводить к StackOverflowException
        var allValues = contextA.GetAllValues().ToList();

        // В наших классах A и B нет примитивных полей, поэтому значений быть не должно
        Assert.Empty(allValues);
    }

    [Fact]
    public void Generate_RecursiveStructure_Snapshot_FromType_CreatesStructure()
    {
        // Act
        var contextA = typeof(A).ToSnapshotContext(_schemaRegistry);

        // Assert
        Assert.NotNull(contextA);

        // Должен создать структуру для B
        var contextB = contextA.GetContext("B");
        Assert.NotNull(contextB);

        // B должен ссылаться на A
        var contextAFromB = contextB.GetContext("A");
        Assert.NotNull(contextAFromB);
    }

    [Fact]
    public void Generate_ComplexRecursiveStructure_LiveContext_RegistryConsistency()
    {
        // Arrange
        var a1 = new A();
        var b1 = new B();
        var a2 = new A();
        var b2 = new B();

        a1.B = b1;
        b1.A = a2;
        a2.B = b2;
        b2.A = a1; // Замыкаем цикл

        // Act
        var contextA1 = a1.ToLiveContext(_schemaRegistry, _contextRegistry);

        // Assert - проверяем консистентность реестра
        var contextB1 = contextA1.GetContext("B");
        var contextA2 = contextB1.GetContext("A");
        var contextB2 = contextA2.GetContext("B");
        var contextA1FromCycle = contextB2.GetContext("A");

        // Все контексты должны быть не null
        Assert.NotNull(contextB1);
        Assert.NotNull(contextA2);
        Assert.NotNull(contextB2);
        Assert.NotNull(contextA1FromCycle);

        // И контекст A1 из цикла должен быть тем же объектом
        Assert.Same(contextA1, contextA1FromCycle);

        // Проверяем, что в реестре корректно отображены связи
        var a1FromRegistry = _contextRegistry.Get(a1);
        var b1FromRegistry = _contextRegistry.Get(b1);
        var a2FromRegistry = _contextRegistry.Get(a2);
        var b2FromRegistry = _contextRegistry.Get(b2);

        Assert.Same(contextA1, a1FromRegistry);
        Assert.Same(contextB1, b1FromRegistry);
        Assert.Same(contextA2, a2FromRegistry);
        Assert.Same(contextB2, b2FromRegistry);
    }
}