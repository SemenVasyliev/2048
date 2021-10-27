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
        int num = 0;

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

        public static SolidColorBrush GetNoneTitleBlocksColor(Block blk)
        {
            return NoneTitle;
        }
    }
}
