using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2048.DCT
{
    class Block
    {
        public int num = 0;
        public bool NewBlock = false;
        public bool Combined = false;
        public enum Direction { Up = 1, Down = 2, Left = 3, Right = 4 }

        private static readonly SolidColorBrush Tile2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ebe6df"));
        private static readonly SolidColorBrush Tile4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c9baa5"));
        private static readonly SolidColorBrush Tile8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f2b580"));
        private static readonly SolidColorBrush Tile16 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d9906a"));
        private static readonly SolidColorBrush Tile32 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d97862"));
        private static readonly SolidColorBrush Tile64 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d45a3d"));
        private static readonly SolidColorBrush Tile128 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7dc88"));
        private static readonly SolidColorBrush Tile256 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a68b33"));
        private static readonly SolidColorBrush Tile512 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fad869"));
        private static readonly SolidColorBrush Tile1024 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5cc42"));
        private static readonly SolidColorBrush Tile2048 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d6b12f"));
        private static readonly SolidColorBrush TileSuper = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3A32"));

        private static readonly SolidColorBrush Border = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        private static readonly SolidColorBrush NoneTitle = new SolidColorBrush((Color)ColorConverter.ConvertFromString("LightGray"));

        public static SolidColorBrush GetTitleBlocksColor(Block blk)
        {
            switch (blk.num)
            {
                case 2:
                    return Tile2;
                case 4:
                    return Tile4;
                case 8:
                    return Tile8;
                case 16:
                    return Tile16;
                case 32:
                    return Tile32;
                case 64:
                    return Tile64;
                case 128:
                    return Tile128;
                case 256:
                    return Tile256;
                case 512:
                    return Tile512;
                case 1024:
                    return Tile1024;
                case 2048:
                    return Tile2048;
                default:
                    return TileSuper;

            }
        }

        public static SolidColorBrush GetNoneTitleBlocksColor()
        {
            return NoneTitle;
        }

        public static void GenerateABlock(ref Block[,] blks)
        {
            int k = Block.CountBlocksNumberZero(blks);
            var ran = new Random();
            int RanPos = ran.Next(0, k);
            int RanNum = ran.Next(1, 2) * 2;

            int temp_count = -1;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                    {
                        temp_count++;
                        if (temp_count == RanPos)
                        {
                            blks[row, col].num = RanNum;
                            blks[row, col].NewBlock = true;
                        }
                    }
                }
            }
        }
        private static int CountBlocksNumberZero(Block[,] blks)
        {
            int kq = 0;
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (blks[row, col].num == 0)
                        kq++;
            return kq;
        }
        public static void InitNewGameBlocks(ref Block[,] blks)
        {
            InitBlocks(ref blks);
            GenerateABlock(ref blks);
            GenerateABlock(ref blks);
        }

        public static void InitBlocks(ref Block[,] blks)
        {
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    blks[row, col] = new Block();
        }

        public static void CopyBlock(ref Block[,] DesBlks, ref Block[,] SouBlks)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    DesBlks[row, col].num = SouBlks[row, col].num;
                    DesBlks[row, col].Combined = SouBlks[row, col].Combined;
                    DesBlks[row, col].NewBlock = SouBlks[row, col].NewBlock;
                }
            }
        }

        private static List<int> ProcessList(ref List<int> ls, ref int score)  
        {
            List<int> result = new List<int>();
            int front = 0, end = 1;
            while (end <= 3 && front <= 2)
            {
                if (ls[front] == 0)
                {
                    front++;
                    end = front + 1;
                }
                else if (ls[end] == 0)
                {
                    end++;
                }
                else
                {
                    if (ls[front] == ls[end]) 
                    {
                        result.Add(front);
                        ls[front] = ls[front] * 2;
                        ls[end] = 0;
                        score = score + ls[front];
                        front++;
                        end = front + 1;
                    }
                    else
                    {
                        front++;
                        end = front + 1;
                    }
                }
            }
            int ZeroIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                if (ls[i] == 0)
                {
                    if (ZeroIndex == -1)
                        ZeroIndex = i;
                }
                else
                {
                    if (ZeroIndex != -1)
                    {
                        if (result.Contains(i))
                        {
                            result[result.IndexOf(i)] = ZeroIndex;
                        }
                        ls[ZeroIndex] = ls[i];
                        ls[i] = 0;
                        ZeroIndex = -1;
                        i = ZeroIndex + 1;
                        result.Add(-1);
                    }
                }
            }
            return result;
        }
        public static bool TryUp(ref Block[,] blks, ref Block[,] OldBlocks, ref int score) 
        {
            Block[,] TempBlocks = new Block[4, 4];
            InitBlocks(ref TempBlocks);
            CopyBlock(ref TempBlocks, ref blks);

            bool BlocksChanged = false;
            for (int col = 0; col < 4; col++)
            {
                List<int> ls = new List<int>();
                for (int row = 0; row < 4; row++)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].Combined = false;
                    blks[row, col].NewBlock = false;
                }
                List<int> ChangedList = ProcessList(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[ChangedList[i], col].Combined = true;
                    }
                    int ls_k = 0;
                    for (int row = 0; row < 4; row++) 
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                CopyBlock(ref OldBlocks, ref TempBlocks);
            }
            return BlocksChanged;
        }
        public static bool TryDown(ref Block[,] blks, ref Block[,] OldBlocks, ref int score) 
        {
            Block[,] TempBlocks = new Block[4, 4];
            InitBlocks(ref TempBlocks);
            CopyBlock(ref TempBlocks, ref blks);

            bool BlocksChanged = false;
            for (int col = 0; col < 4; col++)
            {
                List<int> ls = new List<int>();
                for (int row = 3; row >= 0; row--)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].Combined = false;
                    blks[row, col].NewBlock = false;
                }
                List<int> ChangedList = ProcessList(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[3 - ChangedList[i], col].Combined = true;
                    }
                    int ls_k = 0;
                    for (int row = 3; row >= 0; row--)
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                }
                ls.Clear();
            }

            if (BlocksChanged == true)
            {
                CopyBlock(ref OldBlocks, ref TempBlocks);
            }
            return BlocksChanged;
        }
        public static bool TryLeft(ref Block[,] blks, ref Block[,] OldBlocks, ref int score) 
        {     
            Block[,] TempBlocks = new Block[4, 4];
            InitBlocks(ref TempBlocks);
            CopyBlock(ref TempBlocks, ref blks);

            bool BlocksChanged = false;
            for (int row = 0; row < 4; row++)
            {
                List<int> ls = new List<int>();
                for (int col = 0; col < 4; col++)
                {
                    ls.Add(blks[row, col].num);
  
                    blks[row, col].Combined = false;
                    blks[row, col].NewBlock = false;
                }
                List<int> ChangedList = ProcessList(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[row, ChangedList[i]].Combined = true;
                    }
                    int ls_k = 0;
                    for (int col = 0; col < 4; col++)
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                CopyBlock(ref OldBlocks, ref TempBlocks);
            }
            return BlocksChanged;
        }
        public static bool TryRight(ref Block[,] blks, ref Block[,] OldBlocks, ref int score) 
        {
            Block[,] TempBlocks = new Block[4, 4];
            InitBlocks(ref TempBlocks);
            CopyBlock(ref TempBlocks, ref blks);

            bool BlocksChanged = false;
            for (int row = 0; row < 4; row++)
            {
                List<int> ls = new List<int>();
                for (int col = 3; col >= 0; col--)
                {
                    ls.Add(blks[row, col].num);
                    blks[row, col].Combined = false;
                    blks[row, col].NewBlock = false;
                }
                List<int> ChangedList = ProcessList(ref ls, ref score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            blks[row, 3 - ChangedList[i]].Combined = true;
                    }
                    int ls_k = 0;
                    for (int col = 3; col >= 0; col--)
                    {
                        blks[row, col].num = ls[ls_k];
                        ls_k++;
                    }
                    ls.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                CopyBlock(ref OldBlocks, ref TempBlocks);
            }
            return BlocksChanged;
        }
    }
}
