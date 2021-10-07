using DiceGame.Game;
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

namespace DiceGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameController controller;

        public MainWindow()
        {
            InitializeComponent();

            controller = new GameController(this, 2);
            DrawGameArea(controller.Board);
            controller.Roll();
        }

        public void ShowRollResults(IEnumerable<Direction> directions, int power)
        {
            MessageBox.Show(string.Format("Directions: {0}, Power: {1}", directions, power));
        }

        public void DrawGameArea(Board board)
        {
            for(int x = 0; x < 50; x++)
            {
                for(int y = 0; y < 50; y++)
                {
                    var rect = new Rectangle
                    {
                        Width = 20,
                        Height = 20,
                        Stroke = Brushes.Black
                    };
                    
                    GameArea.Children.Add(rect);
                    Canvas.SetTop(rect, y * 20);
                    Canvas.SetLeft(rect, x * 20);
                }

            }
        }
    }
}
