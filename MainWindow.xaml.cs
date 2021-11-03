using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace _2048.DCT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score;
        Block[,] blks = new Block[4, 4];
        Block[,] OldBlks = new Block[4, 4];
        int PrevScore;
        Button[,] Btns = new Button[4, 4];

        public MainWindow()
        {
            InitializeComponent();
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            //Default Score
            score = 0;
            //Create new blocks
            Block.InitNewGameBlocks(ref blks);
            Block.InitBlocks(ref OldBlks);
            Block.CopyBlock(ref OldBlks, ref blks);
            Score.Text = score.ToString();
            DrawNewBlock();
        }

        private void Mgrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                default:
                    break;
            }
            if (GameOver(blks))
            {
                MessageBox.Show("Game over! Score: " + score.ToString(), "Notification");
            }
        }

        private void MoveLeft()
        {
            PrevScore = score;
            if (Block.TryLeft(ref blks, ref OldBlks, ref score) == true)  
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }

        private void MoveRight()
        {
            PrevScore = score;
            if (Block.TryRight(ref blks, ref OldBlks, ref score) == true)  
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }

        private void MoveDown()
        {
            PrevScore = score;
            if (Block.TryDown(ref blks, ref OldBlks, ref score) == true) 
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }

        private void MoveUp()
        {
            PrevScore = score;
            if (Block.TryUp(ref blks, ref OldBlks, ref score) == true)
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }

        private void DrawNewBlock()
        {
            GridClear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Btns[row, col] = new Button();
                    Btns[row, col].BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
                    Btns[row, col].BorderThickness = new Thickness(3);
                    Btns[row, col].FontStretch = new FontStretch();
                    if (blks[row, col].num != 0) 
                    {
                        Btns[row, col].Background = Block.GetTitleBlocksColor(blks[row, col]);
                        Btns[row, col].Content = blks[row, col].num.ToString();
                        Btns[row, col].FontSize = 40;                       
                    }
                    else
                    {
                        Btns[row, col].Background = Block.GetNoneTitleBlocksColor();
                    }
                    Grid.SetColumn(Btns[row, col], col);
                    Grid.SetRow(Btns[row, col], row);
                    mgrid.Children.Add(Btns[row, col]);
                }
            }
        }
        private void GridClear()
        {
            for (int i = 0; i < mgrid.Children.Count; i++)
            {
                if ((Grid.GetColumn(mgrid.Children[i]) != 4))
                {
                    mgrid.Children.Remove(mgrid.Children[i]);
                }
            }
        }
        private bool GameOver(Block[,] blks)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                        return false;
                }
            }
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (blks[row, col].num == blks[row, col + 1].num)
                        return false;
                }
            }
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (blks[row + 1, col].num == blks[row, col].num)
                        return false;
                }
            }
            return true;
        }

    }

}
