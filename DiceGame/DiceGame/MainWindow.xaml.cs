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
        private const int GridTileSize = 15;

        private GameController controller;

        public MainWindow()
        {
            InitializeComponent();

            controller = new GameController(this, 2);
            DataContext = controller;
            DrawGameArea(controller.Board);
            ShowTurnStartControls();
        }

        private void DrawGameArea(Board board)
        {
            for (int x = 0; x < board.BoardWidth; x++)
            {
                for (int y = 0; y < board.BoardHeight; y++)
                {
                    var rect = CreateTile(board.GetTile(x, y));
                    GameArea.Children.Add(rect);
                    Canvas.SetTop(rect, y * GridTileSize);
                    Canvas.SetLeft(rect, x * GridTileSize);
                }
            }

            DrawPlayers(controller.Players);

            GameArea.Width = board.BoardWidth * GridTileSize;
            GameArea.Height = board.BoardHeight * GridTileSize;
        }

        private Rectangle CreateTile(ITile tile)
            => new Rectangle
            {
                Width = GridTileSize,
                Height = GridTileSize,
                Stroke = Brushes.Black,
                StrokeThickness = 0.5,
                Fill = tile switch
                {
                    BonusTile => Brushes.Blue,
                    MerchantTile => Brushes.Orange,
                    FinalTile => Brushes.Red,
                    _ => Brushes.White
                }
            };

        private void DrawPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                (int x, int y) = player.Position;
                var textBlock = CreatePlayer(player);

                GameArea.Children.Add(textBlock);
                Canvas.SetTop(textBlock, y * GridTileSize);
                Canvas.SetLeft(textBlock, (x + 0.3) * GridTileSize);
            }
        }

        private TextBlock CreatePlayer(Player player)
            => new TextBlock
            {
                Text = player.PlayerNumber.ToString()
            };

        public void ShowTurnStartControls()
        {
            Controls.Child = new TurnStartControl(controller);
        }

        public void ShowBonusControls()
        {
            Controls.Child = new BonusControls();
        }

        public void ShowMerchantControls()
        {
            Controls.Child = new MerchantControls();
        }

        public void ShowDirectionControls(List<Direction> directions)
        {
            Controls.Child = new DirectionControls(controller, directions);
        }
    }
}
