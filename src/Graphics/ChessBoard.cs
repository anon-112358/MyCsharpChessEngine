namespace MyCsharpChessEngine.src.Graphics;

using Raylib_cs;

public sealed class ChessBoard
{
    private ChessBoard() { }

    public static ChessBoard Instance
    {
        get { return Nested.instance; }
    }

    private class Nested
    {
        static Nested() { }

        internal static readonly ChessBoard instance = new();
    }

    public void InitScreen()
    {
        Raylib.InitWindow(Common.windowSize, Common.windowSize, "Chess");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
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
                Console.WriteLine($"y {y} x {x}");
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
