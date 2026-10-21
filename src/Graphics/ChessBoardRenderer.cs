namespace MyCsharpChessEngine.src.Graphics;

using MyCsharpChessEngine.src.Core;
using Raylib_cs;

public sealed class ChessBoardRenderer
{
    private static ChessBoardRenderer? _instance;
    private readonly Task logicThread;

    private ChessBoardRenderer(Task logicThreadInput)
    {
        logicThread = logicThreadInput;
        if (Settings.IsHeadless)
            InitScreen();
    }

    public static ChessBoardRenderer Initialize(Task logicThread)
    {
        if (_instance != null)
            throw new InvalidOperationException("ChessBoardRenderer class already initialized.");

        _instance = new ChessBoardRenderer(logicThread);
        return _instance;
    }

    public static ChessBoardRenderer Instance =>
        _instance
        ?? throw new InvalidOperationException(
            "ChessBoardRenderer.Initialize(logicThread) must be called before accessing Instance."
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
