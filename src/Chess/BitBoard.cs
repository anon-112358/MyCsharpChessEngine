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

        // !fixme! todo: add an efficient "chech if square [x] is 1 on this ulong" helper func<int, bool>
        // !fixme! todo: reduce code repetition

        private void UpdateWhiteMasks()
        {
            WhitePieces = (
                WhitePawns | WhiteKnights | WhiteBishops | WhiteRooks | WhiteQueens | WhiteKing
            );
            UpdateGlobalMasks();
        }

        private void UpdateBlackMasks()
        {
            BlackPieces = (
                BlackPawns | BlackKnights | BlackBishops | BlackRooks | BlackQueens | BlackKing
            );
            UpdateGlobalMasks();
        }

        private void UpdateGlobalMasks()
        {
            AllPieces = (WhitePieces | BlackPieces);
        }

        public ulong WhitePawns
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong WhiteRooks
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong WhiteBishops
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong WhiteKnights
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong WhiteQueens
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong WhiteKing
        {
            get;
            set
            {
                field = value;
                UpdateWhiteMasks();
            }
        } = 0;
        public ulong BlackPawns
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;
        public ulong BlackRooks
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;
        public ulong BlackBishops
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;
        public ulong BlackKnights
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;
        public ulong BlackQueens
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;
        public ulong BlackKing
        {
            get;
            set
            {
                field = value;
                UpdateBlackMasks();
            }
        } = 0;

        public ulong WhitePieces { get; private set; } = 0;
        public ulong BlackPieces { get; private set; } = 0;
        public ulong AllPieces { get; private set; } = 0;
    }
}
