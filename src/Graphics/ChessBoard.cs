namespace MyCsharpChessEngine.src.Graphics;

using Raylib_cs;

public sealed class ChessBoard
{
    private ChessBoard() { } //needs state later for pieces n stuff

    public static ChessBoard Instance
    {
        get { return Nested.instance; }
    }

    private class Nested
    {
        static Nested() { }

        internal static readonly ChessBoard instance = new();
    }

    public void InitScreen(Task logicThread) // maybe will use state
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
