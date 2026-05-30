using System.Collections;
using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Identification.Identifiable.Realizations;
using GMutagenEngine.Mediators;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Middlewares.Sync.Interfaces;
using GMutagenEngine.Schemas.Extraction.Api;
using GMutagenEngine.Storing.Registries.Sync.Indexed;
using GMutagenEngine.Storing.Storages.Sync.Indexed;
using GMutagenEngine.Values.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GMutagenEngine;

public class Boot
{
    public void Start()
    {
        var serviceCollection = new ServiceCollection();

        var schemaExtractor = SchemaExtractorInitializationExtensions.CreateDefault();
        
        var mediator = FrameworkMediatorFactory.CreateDefaultMediator(
            string.Empty,
            out var publishRegistry,
            out var publishMediator,
            out var publishWithInputRegistry,
            out var publishWithInputMediator,
            out var sendRegistry,
            out var sendMediator,
            out var sendWithInputRegistry,
            out var sendWithInputMediator,
            out var publishPipelineRegistry,
            out var publishPipeline,
            out var publishWithInputPipelineRegistry,
            out var publishWithInputPipeline,
            out var sendPipelineRegistry,
            out var sendPipeline,
            out var sendWithInputPipelineRegistry,
            out var sendWithInputPipeline,
            out var mediatorWithMiddleware,
            out var publishDefault,
            out var publishWithInputDefault,
            out var sendDefault,
            out var sendWithInputDefault
        );
    }
}

public sealed class KeyPressed
{
    public KeyboardKeyCode KeyboardKeyCode { get; set; } = KeyboardKeyCode.None;
}

public sealed record KeyState(KeyboardKeyCode KeyboardKeyCode, KeyboardKeyState State);

public enum KeyboardKeyState
{
    Begin,
    Pressed,
    End,
    Released,
}


public class Selector<TId, TValue, TStorageId>(IValue<TStorageId> current, IIndexedSyncRegistry<TStorageId, IIndexedSyncRegistry<TId, TValue>> registry) : IIndexedSyncRegistry<TId, TValue>
{
    public IEnumerator<KeyValuePair<TId, TValue>> GetEnumerator()
        => GetStorage().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetStorage().GetEnumerator();

    public TValue GetBy(Func<TValue, bool> predicate)
        => GetStorage().GetBy(predicate);

    public bool TryGetBy(Func<TValue, bool> predicate, out TValue value)
        => GetStorage().TryGetBy(predicate, out value);

    public IEnumerable<TValue> GetAllBy(Func<TValue, bool> predicate)
        => GetStorage().GetAllBy(predicate);

    public IEnumerable<TValue> GetAll()
        => GetStorage().GetAll();

    public bool ExistsBy(Func<TValue, bool> predicate)
        => GetStorage().ExistsBy(predicate);

    public TValue Get(TId? id)
        => GetStorage().Get(id);

    public bool TryGet(TId? id, out TValue entity)
        => GetStorage().TryGet(id, out entity);

    public bool Exists(TId? id)
        => GetStorage().Exists(id);

    public IIndexedSyncRegistry<TId, TValue> GetStorage()
        => registry.Get(current.Value);
}


public sealed class KeyboardInputReader(ISyncMediator mediator) : ISyncActionHandler
{
    private readonly KeyPressed _keyPressed = new();

    public void Handle()
    {
        var keys = Array.Empty<KeyboardKeyCode>();
        foreach (var key in keys)
        {
            _keyPressed.KeyboardKeyCode = key;
            mediator.Publish(_keyPressed);
        }
    }
}

public sealed class StoreKeyboardKeyInfo(IIndexedSyncStorage<KeyboardKeyCode, bool> curStates)
    : ISyncActionHandler<KeyPressed>
{
    public void Handle(KeyPressed data)
        => curStates.Add(data.KeyboardKeyCode, true);
}

public sealed class KeyboardPollingHandler(
    ISyncMediator mediator,
    IIndexedSyncStorage<KeyboardKeyCode, bool> prevStates,
    IIndexedSyncStorage<KeyboardKeyCode, bool> curStates
) : ISyncActionHandler
{
    private readonly IEnumerable<KeyboardKeyCode> _codes = Enum.GetValues<KeyboardKeyCode>();

    public void Handle()
    {
        foreach (var key in _codes)
        {
            var wasPressed = prevStates.TryGet(key, out var prev) && prev;
            var isPressed = curStates.TryGet(key, out var cur) && cur;

            switch (wasPressed)
            {
                case false when isPressed:
                    mediator.Publish(new KeyState(key, KeyboardKeyState.Begin));
                    break;
                case true when isPressed:
                    mediator.Publish(new KeyState(key, KeyboardKeyState.Pressed));
                    break;
                case true when !isPressed:
                    mediator.Publish(new KeyState(key, KeyboardKeyState.End));
                    mediator.Publish(new KeyState(key, KeyboardKeyState.Released));
                    break;
            }
        }
    }
}

public enum KeyboardKeyCode
{
    None,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,

    Digit0,
    Digit1,
    Digit2,
    Digit3,
    Digit4,
    Digit5,
    Digit6,
    Digit7,
    Digit8,
    Digit9,

    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,

    LeftShift,
    RightShift,
    LeftControl,
    RightControl,
    LeftAlt,
    RightAlt,
    LeftMeta,
    RightMeta,
    Fn,
    CapsLock,

    Escape,
    Enter,
    Space,
    Tab,
    Backspace,
    Delete,
    Insert,
    Home,
    End,
    PageUp,
    PageDown,

    UpArrow,
    DownArrow,
    LeftArrow,
    RightArrow,

    PrintScreen,
    ScrollLock,
    Pause,
    NumLock,
    ContextMenu,

    NumPad0,
    NumPad1,
    NumPad2,
    NumPad3,
    NumPad4,
    NumPad5,
    NumPad6,
    NumPad7,
    NumPad8,
    NumPad9,
    NumPadAdd,
    NumPadSubtract,
    NumPadMultiply,
    NumPadDivide,
    NumPadDecimal,
    NumPadEnter,

    Minus,
    Equals,
    LeftBracket,
    RightBracket,
    Backslash,
    Semicolon,
    Quote,
    Comma,
    Period,
    Slash,
    Backtick
}

public static class FrameworkMediatorFactory
{
    public static ISyncMediator CreateDefaultMediator<TId>(
        TId defaultId,
        out IIndexedSyncRegistry<TId, ISyncActionHandler> publishRegistry,
        out ISyncMediatorPublish<TId> publishMediator,
        out IIndexedSyncRegistry<TId, ISyncActionHandlerIn> publishWithInputRegistry,
        out ISyncMediatorPublishWithInput<TId> publishWithInputMediator,
        out IIndexedSyncRegistry<TId, ISyncFuncHandlerOut> sendRegistry,
        out ISyncMediatorSend<TId> sendMediator,
        out IIndexedSyncStorage<TId, ISyncFuncHandlerInOut> sendWithInputRegistry,
        out ISyncMediatorSendWithInput<TId> sendWithInputMediator,
        out IIndexedSyncRegistry<TId, IEnumerable<IMiddleware>> publishPipelineRegistry,
        out SyncMediatorPublishPipeline<TId> publishPipeline,
        out IIndexedSyncRegistry<TId, IEnumerable<IInMiddleware>> publishWithInputPipelineRegistry,
        out SyncMediatorPublishWithInputPipeline<TId> publishWithInputPipeline,
        out IIndexedSyncRegistry<TId, IEnumerable<IOutMiddleware>> sendPipelineRegistry,
        out SyncMediatorSendPipeline<TId> sendPipeline,
        out IIndexedSyncStorage<TId, IEnumerable<IInOutMiddleware>> sendWithInputPipelineRegistry,
        out SyncMediatorSendWithInputPipeline<TId> sendWithInputPipeline,
        out SyncMediator<TId> mediatorWithMiddleware,
        out SyncDefaultIdPublish<TId> publishDefault,
        out SyncDefaultIdPublishWithInput<TId> publishWithInputDefault,
        out SyncDefaultIdSend<TId> sendDefault,
        out SyncDefaultIdSendWithInput<TId> sendWithInputDefault
    ) where TId : notnull
    {
        publishRegistry = new InMemoryIndexedSyncRegistry<TId, ISyncActionHandler>();
        publishMediator = new SyncMediatorPublish<TId>(publishRegistry);

        publishWithInputRegistry = new InMemoryIndexedSyncRegistry<TId, ISyncActionHandlerIn>();
        publishWithInputMediator = new SyncMediatorPublishWithInput<TId>(publishWithInputRegistry);

        sendRegistry = new InMemoryIndexedSyncRegistry<TId, ISyncFuncHandlerOut>();
        sendMediator = new SyncMediatorSend<TId>(sendRegistry);

        sendWithInputRegistry = new InMemoryIndexedSyncStorage<TId, ISyncFuncHandlerInOut>();
        sendWithInputMediator = new SyncMediatorSendWithInput<TId>(sendWithInputRegistry);

        publishPipelineRegistry = new InMemoryIndexedSyncRegistry<TId, IEnumerable<IMiddleware>>();
        publishPipeline = new SyncMediatorPublishPipeline<TId>(publishMediator, publishPipelineRegistry);

        publishWithInputPipelineRegistry = new InMemoryIndexedSyncRegistry<TId, IEnumerable<IInMiddleware>>();
        publishWithInputPipeline =
            new SyncMediatorPublishWithInputPipeline<TId>(publishWithInputMediator, publishWithInputPipelineRegistry);

        sendPipelineRegistry = new InMemoryIndexedSyncRegistry<TId, IEnumerable<IOutMiddleware>>();
        sendPipeline = new SyncMediatorSendPipeline<TId>(sendMediator, sendPipelineRegistry);

        sendWithInputPipelineRegistry = new InMemoryIndexedSyncStorage<TId, IEnumerable<IInOutMiddleware>>();
        sendWithInputPipeline =
            new SyncMediatorSendWithInputPipeline<TId>(sendWithInputMediator, sendWithInputPipelineRegistry);

        mediatorWithMiddleware = new SyncMediator<TId>(
            publishPipeline,
            publishWithInputPipeline,
            sendPipeline,
            sendWithInputPipeline
        );

        publishDefault = new SyncDefaultIdPublish<TId>(publishPipeline, defaultId);
        publishWithInputDefault = new SyncDefaultIdPublishWithInput<TId>(publishWithInputPipeline, defaultId);
        sendDefault = new SyncDefaultIdSend<TId>(sendPipeline, defaultId);
        sendWithInputDefault = new SyncDefaultIdSendWithInput<TId>(sendWithInputPipeline, defaultId);

        return new SyncMediator(
            publishDefault,
            publishWithInputDefault,
            sendDefault,
            sendWithInputDefault
        );
    }
}

public interface IConfigProvider : IConfigProviderMark {
    void Load();
    void Unload();
}

public sealed class SyncConfigService<TId, TProviderId, TEntity>(
    IIndexedSyncStorage<TId, TEntity> storage,
    IIndexedSyncStorage<TProviderId, IConfigProvider> providersStorage)
    where TId : notnull
    where TProviderId : notnull
{
    public void AddProvider(TProviderId providerId, IConfigProvider provider)
    {
        providersStorage.Add(providerId, provider);
    }

    public bool RemoveProvider(TProviderId providerId)
    {
        if (!providersStorage.TryGet(providerId, out var provider))
            return false;

        provider.Unload();
        providersStorage.Remove(providerId);
        return true;
    }

    public void LoadProvider(TProviderId providerId)
    {
        if (!providersStorage.TryGet(providerId, out var provider))
            return;

        provider.Load();
    }

    public void UnloadProvider(TProviderId providerId)
    {
        if (!providersStorage.TryGet(providerId, out var provider))
            return;

        provider.Unload();
    }

    public void LoadAllProviders()
    {
        foreach (var kv in providersStorage)
            kv.Value.Load();
    }

    public void UnloadAllProviders()
    {
        foreach (var kv in providersStorage)
            kv.Value.Unload();
    }
}

public class DictionaryProvider<TId, TKey, TValue>(
    TId id,
    Dictionary<TKey, TValue> values,
    IIndexedSyncStorage<TKey, TValue> storage)
    : Identifiable<TId>(id), IConfigProvider
    where TKey : notnull
{
    public void Load()
    {
        foreach (var kv in values)
            storage.Add(kv.Key, kv.Value);
    }

    public void Unload()
    {
        foreach (var kv in values)
            storage.Remove(kv.Key);
    }
}
public interface IConfigProviderMark : ISelfMark<IConfigProviderMark> {
}
