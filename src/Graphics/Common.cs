using Raylib_cs;

namespace MyCsharpChessEngine.src.Graphics;

static class Common
{
    public static readonly Color BlackTileColor = new(181, 136, 99);
    public static readonly Color WhiteTileColor = new(240, 217, 181);
    public static readonly Color backgroundColor = new(22, 21, 18);

    public static readonly int TileSize = 64;

    public static readonly int PaddingX = 0;
    public static readonly int PaddingY = 0;

    public static readonly int boardSize = TileSize * 8;
}
