using System.Collections.Concurrent;

namespace MyCsharpChessEngine.src.Graphics;

public sealed class SingleThreadAsyncContext : SynchronizationContext
{
    public static class SingleThreadRunner
    {
        public static void Run(Func<Task> entryPoint)
        {
            var prevCtx = SynchronizationContext.Current;
            var context = new SingleThreadAsyncContext();
            SynchronizationContext.SetSynchronizationContext(context);

            try
            {
                var task = entryPoint();
                task.ContinueWith(_ => context.Complete(), TaskScheduler.Default);
                context.RunOnCurrentThread();
                task.GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(prevCtx);
            }
        }
    }

    private readonly BlockingCollection<(SendOrPostCallback Callback, object? State)> _queue = [];

    public override void Post(SendOrPostCallback d, object? state)
    {
        _queue.Add((d, state));
    }

    public override void Send(SendOrPostCallback d, object? state)
    {
        d(state);
    }

    public void RunOnCurrentThread()
    {
        while (_queue.TryTake(out var item, Timeout.Infinite))
        {
            item.Callback(item.State);
        }
    }

    public void Complete() => _queue.CompleteAdding();
}
