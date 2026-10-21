using MyCsharpChessEngine.src.Graphics;

async Task testFunc()
{
    Console.WriteLine("presup");
    await Task.Delay(5000);
    throw new Exception("sup");
}

var logicTask = Task.Run(() => testFunc());

var board = ChessBoard.Instance;

// todo fix this freezing
board.InitScreen(logicTask);

if (logicTask.IsFaulted)
{
    await logicTask;
}
