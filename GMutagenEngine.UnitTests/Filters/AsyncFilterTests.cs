using Filters.Async.Api;
using GMutagenEngine.Handlers.Async.Funcs.Realizations;

namespace GMutagenEngine.UnitTests.Filters;

[TestFixture]
public class AsyncFilterTests
{
    [Test]
    public async Task And_BothTrue_ReturnsTrue()
    {
        var f1 = new AsyncFuncHandler<int, bool>(async (x, _) =>
        {
            await Task.Yield();
            return x > 0;
        });

        var f2 = new AsyncFuncHandler<int, bool>(async (x, _) =>
        {
            await Task.Yield();
            return x < 10;
        });

        var filter = f1.And(f2);

        var result = await filter.Handle(5, CancellationToken.None);

        Assert.IsTrue(result);
    }

    [Test]
    public async Task And_ShortCircuit_Works()
    {
        bool secondCalled = false;

        var f1 = new AsyncFuncHandler<int, bool>((_, _) => Task.FromResult(false));
        var f2 = new AsyncFuncHandler<int, bool>((_, _) =>
        {
            secondCalled = true;
            return Task.FromResult(true);
        });

        var filter = f1.And(f2);
        var result = await filter.Handle(5, CancellationToken.None);

        Assert.IsFalse(result);
        Assert.IsFalse(secondCalled);
    }

    [Test]
    public async Task Or_FirstTrue_ShortCircuits()
    {
        bool secondCalled = false;

        var f1 = new AsyncFuncHandler<int, bool>((_, _) => Task.FromResult(true));
        var f2 = new AsyncFuncHandler<int, bool>((_, _) =>
        {
            secondCalled = true;
            return Task.FromResult(false);
        });

        var filter = f1.Or(f2);
        var result = await filter.Handle(5, CancellationToken.None);

        Assert.IsTrue(result);
        Assert.IsFalse(secondCalled);
    }

    [Test]
    public async Task Not_InvertsResult()
    {
        var f = new AsyncFuncHandler<int, bool>((x, _) => Task.FromResult(x == 0));
        var not = f.Not();

        Assert.IsTrue(await not.Handle(1, CancellationToken.None));
        Assert.IsFalse(await not.Handle(0, CancellationToken.None));
    }
}