using System.CommandLine;
using MyCsharpChessEngine.src.Graphics;

namespace MyCsharpChessEngine.src.Core
{
    public static class PlayAction
    {
        public static async Task Run(ParseResult _, CancellationToken token)
        {
            async Task testFunc()
            {
                Console.WriteLine("presup");
                await Task.Delay(5000, token);
                throw new Exception("sup");
            }
            var logicTask = Task.Run(() => testFunc(), token);

            ChessBoardRenderer.Initialize(logicTask);
            var board = ChessBoardRenderer.Instance;

            if (logicTask.IsFaulted)
            {
                await logicTask;
            }
        }
    }
}
