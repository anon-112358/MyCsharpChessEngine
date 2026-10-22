namespace MyCsharpChessEngine.src.Graphics;

using System.Diagnostics;
using System.Reflection;
using MyCsharpChessEngine.src.Chess;
using Raylib_cs;

public class ChessBoardRenderer
{
    private readonly Task logicThread;
    private int paddingX = Common.PaddingX;
    private int paddingY = Common.PaddingY;

    private readonly Dictionary<string, Texture2D> pieces = [];

    private static readonly Dictionary<string, Func<BitBoard, ulong>> pieceMasks = new()
    {
        { "w-rook", (obj) => obj.WhiteRooks },
        { "w-bishop", (obj) => obj.WhiteBishops },
        { "w-knight", (obj) => obj.WhiteKnights },
        { "w-queen", (obj) => obj.WhiteQueens },
        { "w-king", (obj) => obj.WhiteKing },
        { "w-pawn", (obj) => obj.WhitePawns },
        { "b-rook", (obj) => obj.BlackRooks },
        { "b-bishop", (obj) => obj.BlackBishops },
        { "b-knight", (obj) => obj.BlackKnights },
        { "b-queen", (obj) => obj.BlackQueens },
        { "b-king", (obj) => obj.BlackKing },
        { "b-pawn", (obj) => obj.BlackPawns },
    };

    public BitBoard board;

    private Texture2D? GetPieceAtSquare(int pos)
    {
        Dictionary<string, ulong> masks = pieceMasks.ToDictionary(
            kv => kv.Key,
            kv => kv.Value(board)
        );
        foreach (var kv in masks)
        {
            if ((kv.Value & (1UL << pos)) != 0)
            {
                return pieces[kv.Key];
            }
        }
        return null;
    }

    private void LoadTextures()
    {
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var pieceName in pieceMasks.Keys)
        {
            if (pieces.TryGetValue(pieceName, out var oldTexture))
                Raylib.UnloadTexture(oldTexture);

            string resourceName = $"MyCsharpChessEngine.Resources.{pieceName}.png";
            using Stream stream =
                assembly.GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException(
                    $"ERROR: Could not load piece {pieceName} piece at {resourceName} path."
                );

            using MemoryStream ms = new();
            stream.CopyTo(ms);
            var data = ms.ToArray();

            var image = Raylib.LoadImageFromMemory(".png", data);
            pieces[pieceName] = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image);
        }
    }

    private void VerifyLoading()
    {
        foreach (var name in pieceMasks.Keys)
        {
            if (!pieces.ContainsKey(name))
                throw new InvalidDataException(
                    $"ERROR: \"{name}\" piece not found in loaded pieces. Contained pieces: {String.Join(", ", pieces.Select((p) => p.Key).ToArray())}"
                );
        }
    }

    public ChessBoardRenderer(Task logicThreadInput)
    {
        board = BitBoard.DefaultBoard;
        logicThread = logicThreadInput;
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
    }

    public async Task InitScreen()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
        Raylib.InitWindow(
            width: Common.boardSize + Common.PaddingX,
            height: Common.boardSize + Common.PaddingY,
            title: "Chess"
        );
        LoadTextures();
        VerifyLoading();

        SingleThreadAsyncContext.SingleThreadRunner.Run(Render);

        Raylib.CloseWindow();
    }

    private async Task Render()
    {
        Stopwatch watch = new();
        watch.Start();

        while (!Raylib.WindowShouldClose() && !logicThread.IsFaulted && !logicThread.IsCanceled)
        {
            watch.Restart();

            int newWidth = (Raylib.GetScreenWidth() - Common.boardSize) / 2;
            int newHeight = (Raylib.GetScreenHeight() - Common.boardSize) / 2;
            paddingX = newWidth > 0 ? newWidth : 0;
            paddingY = newHeight > 0 ? newHeight : 0;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Common.backgroundColor);

            DrawTiles();
            DrawPieces();

            Raylib.EndDrawing();

            var freeTime = (1000 / Common.targetFPS) - (int)(watch.ElapsedMilliseconds);
            //Console.WriteLine($"frame time: {freeTime}ms");
            if (freeTime > 0)
                await Task.Delay(freeTime);
        }
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

    private void DrawPieces()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                var potentialTexture = GetPieceAtSquare(y * 8 + x);
                if (potentialTexture == null)
                    continue;

                var texture = (Texture2D)potentialTexture;
                Raylib.DrawTexture(
                    texture,
                    x * Common.TileSize + paddingX,
                    y * Common.TileSize + paddingY,
                    Color.White
                );
            }
        }
    }
}
