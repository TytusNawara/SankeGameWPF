using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeZadanieRekrutacyjne
{
    public class GameDisplayer
    {
        private Canvas _canvas;
        private GameLogic _gameLogic;

        private List<Shape> _backgroundShapes= new List<Shape>();
        private RectangleDisplayer[,] _rectangles;

        public GameDisplayer(Canvas canvas, GameLogic gameLogic)
        {
            this._canvas = canvas;
            this._gameLogic = gameLogic;
            _backgroundShapes.Add(new Rectangle
            {
                Width = canvas.Width,
                Height = canvas.Height,
                Fill = Brushes.Black
            });
            
            foreach (var shape in _backgroundShapes)
            {
                canvas.Children.Add(shape);
            }

            _rectangles = new RectangleDisplayer[20,20];
            for (int i = 0; i < _rectangles.GetLength(0); i++)
            {
                for (int j = 0; j < _rectangles.GetLength(1); j++)
                {
                    int x =  i * 20 + RectangleDisplayer.InitialSize / 2;
                    int y =  j * 20 + RectangleDisplayer.InitialSize / 2;
                    
                    RectangleDisplayer rectangleDisplayer = new RectangleDisplayer(canvas, x, y);
                    _rectangles[i, j] = rectangleDisplayer;
                }
            }
        }

        const int SnakeSquareSize = 20;

        public EventHandler GetDrawFunction()
        {
            return DrawGameArea;
        }

        private void DrawGameArea(object sender, EventArgs e)
        {
            for (int i = 0; i < _gameLogic.Map.GetLength(0); i++)
            {
                for (int j = 0; j < _gameLogic.Map.GetLength(1); j++)
                {
                    TypeOfField typeOfField = _gameLogic.Map[i, j];
                    if (typeOfField == TypeOfField.Snake)
                    {
                        _rectangles[i, j].Color = Brushes.ForestGreen;
                    }
                    else if (typeOfField == TypeOfField.Apple)
                    {
                        _rectangles[i, j].Color = Brushes.DarkRed;
                    }
                    else
                    {
                        _rectangles[i, j].Color = Brushes.Black;
                    }
                }
            }
        }

        public void ChangeGameLogic(GameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }
    }
}