namespace MyCsharpChessEngine.src.Graphics;

using System.Diagnostics;
using Raylib_cs;

public class ChessBoardRenderer
{
    private readonly Task logicThread;
    private int paddingX = Common.PaddingX;
    private int paddingY = Common.PaddingY;

    private static readonly TaskFactory scheduler = new(
        new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler
    );

    public ChessBoardRenderer(Task logicThreadInput)
    {
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
}
