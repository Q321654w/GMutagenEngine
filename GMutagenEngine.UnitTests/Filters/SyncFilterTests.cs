using Filters.Sync.Api;
using GMutagenEngine.Handlers.Funcs.Realizations;

namespace GMutagenEngine.UnitTests.Filters;

[TestFixture]
public class SyncFilterTests
{
    [Test]
    public void And_BothTrue_ReturnsTrue()
    {
        var f1 = new SyncFuncHandler<int, bool>(x => x > 0);
        var f2 = new SyncFuncHandler<int, bool>(x => x < 10);

        var filter = f1.And(f2);
        filter = filter.And(f2);

        Assert.IsTrue(filter.Handle(5));
        Assert.IsTrue(filter.Handle(5));
    }

    [Test]
    public void And_ShortCircuit_Works()
    {
        bool secondCalled = false;

        var f1 = new SyncFuncHandler<int, bool>(_ => false);
        var f2 = new SyncFuncHandler<int, bool>(_ =>
        {
            secondCalled = true;
            return true;
        });

        var filter = f1.And(f2);
        var result = filter.Handle(5);

        Assert.IsFalse(result);
        Assert.IsFalse(secondCalled);
    }

    [Test]
    public void Or_FirstTrue_ShortCircuits()
    {
        bool secondCalled = false;

        var f1 = new SyncFuncHandler<int, bool>(_ => true);
        var f2 = new SyncFuncHandler<int, bool>(_ =>
        {
            secondCalled = true;
            return false;
        });

        var filter = f1.Or(f2);
        var result = filter.Handle(5);

        Assert.IsTrue(result);
        Assert.IsFalse(secondCalled);
    }

    [Test]
    public void Not_InvertsResult()
    {
        var f = new SyncFuncHandler<int, bool>(x => x == 0);
        var not = f.Not();

        Assert.IsTrue(not.Handle(1));
        Assert.IsFalse(not.Handle(0));
    }
}