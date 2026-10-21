using MyCsharpChessEngine.src.Graphics;

static async Task testFunc()
{
    Console.WriteLine("presup");
    await Task.Delay(5000);
    throw new Exception("sup");
}

var logicTask = Task.Run(() => testFunc());

var board = ChessBoard.Instance;

board.InitScreen(logicTask);

if (logicTask.IsFaulted)
{
    await logicTask;
}
