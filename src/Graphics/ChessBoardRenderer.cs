namespace MyCsharpChessEngine.src.Graphics;

using MyCsharpChessEngine.src.Core;
using Raylib_cs;

public sealed class ChessBoardRenderer
{
    private static ChessBoardRenderer? _instance;
    private readonly Task logicThread;
    private int paddingX = Common.PaddingX;
    private int paddingY = Common.PaddingY;

    private ChessBoardRenderer(Task logicThreadInput)
    {
        logicThread = logicThreadInput;
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
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

    private void InitScreen()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
        Raylib.InitWindow(
            width: Common.boardSize + Common.PaddingX,
            height: Common.boardSize + Common.PaddingY,
            title: "Chess"
        );
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose() && !logicThread.IsFaulted && !logicThread.IsCanceled)
        {
            int newWidth = (Raylib.GetScreenWidth() - Common.boardSize) / 2;
            int newHeight = (Raylib.GetScreenHeight() - Common.boardSize) / 2;
            paddingX = newWidth > 0 ? newWidth : 0;
            paddingY = newHeight > 0 ? newHeight : 0;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Common.backgroundColor);

            DrawTiles();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void DrawTiles()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Raylib.DrawRectangle(
                    x * Common.TileSize + paddingX,
                    y * Common.TileSize + paddingY,
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
