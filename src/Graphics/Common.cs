using Raylib_cs;

namespace MyCsharpChessEngine.src.Graphics;

static class Common
{
    public static readonly Color backgroundColor = new(19, 19, 19);

    public static readonly Color BlackTileColor = new(0, 0, 0);
    public static readonly Color WhiteTileColor = new(255, 255, 255);

    public static readonly int windowSize = 512;
    public static readonly int TileSize = 64;
}
