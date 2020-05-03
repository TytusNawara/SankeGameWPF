using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakeZadanieRekrutacyjne
{
    public partial class MainWindow : Window
    {
        private GameDisplayer _gameDisplayer;
        private GameLogic _gameLogic;
        private System.Windows.Threading.DispatcherTimer _timer;
        private BitmapImage _loadingImage;
        private SnakePicturesProvider _snakePicturesProvider;

        private const string TITLE_PREFIX = "Tytus Nawara snake - Score: ";
        private const string SCORE_BOX_PREFIX = "Score: ";

        public MainWindow()
        {
            InitializeComponent();
            _loadingImage = new BitmapImage(new Uri("Assets/loading.png", UriKind.Relative));
            _snakePicturesProvider = new SnakePicturesProvider();
        }

        private void RenderingFinished(object sender, EventArgs e)
        {
            _gameLogic = new GameLogic();
            _gameDisplayer = new GameDisplayer(GameArea, _gameLogic);
            CompositionTarget.Rendering += _gameDisplayer.GetDrawFunction();

            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _timer.Start();
            _subcribeCurrentGameToEvents();
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            _restartGame();
        }

        private void _updateScorebord()
        {
            string score = _gameLogic.Score.ToString();
            Title = TITLE_PREFIX + score;
            ScoreBox.Text = SCORE_BOX_PREFIX + score;
        }

        private void _gameOver()
        {
            MessageBox.Show("Game Over");
            _restartGame();
        }

        private void _restartGame()
        {
            _unsubcribeCurrentGameToEvents();
            _gameLogic = new GameLogic();
            _subcribeCurrentGameToEvents();
            _updateScorebord();
            _gameDisplayer.ChangeGameLogic(_gameLogic);
        }

        private void _changeSnakePicture()
        {
            SnakeDisplayer.Source = _loadingImage;
            Thread gettingImage = new Thread(() =>
            {
                BitmapImage snakePicture = _snakePicturesProvider.ProvideSnakePicture();
                if (snakePicture != null) { 
                    snakePicture.Freeze();
                    SnakeDisplayer.Dispatcher.Invoke(
                         new Action<BitmapImage>((s) => SnakeDisplayer.Source = s),
                        snakePicture);
                }
            });
            gettingImage.Start();
        }

        private void KeyWasPressed(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _gameLogic.SnakeDirection = Direction.Up;
                    break;
                case Key.Down:
                    _gameLogic.SnakeDirection = Direction.Down;
                    break;
                case Key.Left:
                    _gameLogic.SnakeDirection = Direction.Left;
                    break;
                case Key.Right:
                    _gameLogic.SnakeDirection = Direction.Right;
                    break;
            }
        }

        void _subcribeCurrentGameToEvents()
        {
            _timer.Tick += _gameLogic.TickFrame;
            _gameLogic.ScoredPointEvent += _updateScorebord;
            _gameLogic.ScoredPointEvent += _changeSnakePicture;
            _gameLogic.GameOverEvent += _gameOver;
        }

        void _unsubcribeCurrentGameToEvents()
        {
            _timer.Tick -= _gameLogic.TickFrame;
            _gameLogic.ScoredPointEvent -= _updateScorebord;
            _gameLogic.ScoredPointEvent -= _changeSnakePicture;
            _gameLogic.GameOverEvent -= _gameOver;
        }
    }
}