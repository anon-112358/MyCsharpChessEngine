namespace MyCsharpChessEngine.src.Chess
{
    public class BitBoard
    {
        public static readonly BitBoard DefaultBoard = new()
        {
            WhitePawns = 0x00_00_00_00_00_00_FF_00,
            WhiteRooks = 0x00_00_00_00_00_00_00_81,
            WhiteKnights = 0x00_00_00_00_00_00_00_42,
            WhiteBishops = 0x00_00_00_00_00_00_00_24,
            WhiteQueens = 0x00_00_00_00_00_00_00_08,
            WhiteKing = 0x00_00_00_00_00_00_00_10,
            BlackPawns = 0x00_FF_00_00_00_00_00_00,
            BlackRooks = 0x81_00_00_00_00_00_00_00,
            BlackKnights = 0x42_00_00_00_00_00_00_00,
            BlackBishops = 0x24_00_00_00_00_00_00_00,
            BlackQueens = 0x08_00_00_00_00_00_00_00,
            BlackKing = 0x10_00_00_00_00_00_00_00,
        };

        public ulong WhitePawns = 0;
        public ulong WhiteRooks = 0;
        public ulong WhiteBishops = 0;
        public ulong WhiteKnights = 0;
        public ulong WhiteQueens = 0;
        public ulong WhiteKing = 0;
        public ulong BlackPawns = 0;
        public ulong BlackRooks = 0;
        public ulong BlackBishops = 0;
        public ulong BlackKnights = 0;
        public ulong BlackQueens = 0;
        public ulong BlackKing = 0;

        public ulong AllWhite
        {
            get =>
                (WhitePawns | WhiteKnights | WhiteBishops | WhiteRooks | WhiteQueens | WhiteKing);
        }
        public ulong AllBlack
        {
            get =>
                (BlackPawns | BlackKnights | BlackBishops | BlackRooks | BlackQueens | BlackKing);
        }
        public ulong AllPieces
        {
            get => (AllWhite | AllBlack);
        }
    }
}
