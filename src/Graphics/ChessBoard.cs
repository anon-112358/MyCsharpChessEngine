namespace MyCsharpChessEngine.src.Graphics;

using Raylib_cs;

public sealed class ChessBoard
{
    private static ChessBoard? _instance;
    private readonly Task logicThread;

    private ChessBoard(Task logicThreadInput)
    {
        logicThread = logicThreadInput;
        InitScreen();
    }

    public static ChessBoard Initialize(Task logicThread)
    {
        if (_instance != null)
            throw new InvalidOperationException("ChessBoard already initialized.");

        _instance = new ChessBoard(logicThread);
        return _instance;
    }

    public static ChessBoard Instance =>
        _instance
        ?? throw new InvalidOperationException(
            "ChessBoard.Initialize(logicThread) must be called before accessing Instance."
        );

    private void InitScreen() // maybe will use state
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
        Raylib.InitWindow(Common.windowSize, Common.windowSize, "Chess");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose() && !logicThread.IsFaulted && !logicThread.IsCanceled)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Common.backgroundColor);

            DrawTiles();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static void DrawTiles()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Raylib.DrawRectangle(
                    x * Common.TileSize,
                    y * Common.TileSize,
                    Common.TileSize,
                    Common.TileSize,
                    y % 2 == 0
                        ? (x % 2 == 0 ? Common.WhiteTileColor : Common.BlackTileColor)
                        : (x % 2 != 0 ? Common.WhiteTileColor : Common.BlackTileColor)
                );
            }
        }
    }
}
