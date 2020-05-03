using System;
using System.Collections.Generic;

namespace SnakeZadanieRekrutacyjne
{
    public class GameLogic
    {
        public TypeOfField[,] Map { get;}
        public Direction SnakeDirection { get; set; }
        public int Score
        {
            get => _score;
        }

        public delegate void ScoredPointDelegate();
        public event ScoredPointDelegate ScoredPointEvent;

        public delegate void GameOverDelegate();
        public event GameOverDelegate GameOverEvent;

        public const int SIZE = 20;

        private readonly List<ObjectOnMap> _snake;
        private ObjectOnMap _apple;
        private int _score = 0;

        public void TickFrame(object sender, EventArgs e)
        {
            ObjectOnMap snakeHead = _snake[0];
            switch (SnakeDirection)
            {
                case Direction.Up:
                    snakeHead.Y -= 1;
                    break;
                case Direction.Down:
                    snakeHead.Y += 1;
                    break;
                case Direction.Right:
                    snakeHead.X += 1;
                    break;
                case Direction.Left:
                    snakeHead.X -= 1;
                    break;
            }

            if (snakeHead.X < 0 || snakeHead.X >= SIZE)
            {
                _looseGame();
                return;
            }
            if (snakeHead.Y < 0 || snakeHead.Y >= SIZE)
            {
                _looseGame();
                return;
            }
            if (Map[snakeHead.X, snakeHead.Y] == TypeOfField.Snake)
            {
                _looseGame();
                return;
            }

            if (_apple.X == snakeHead.X && _apple.Y == snakeHead.Y)
            {
                _snake.Insert(0, snakeHead);
                _addSnakeToMap();
                _pointScored();
                return;
            }

            _clearSnakeFromMap();
            
            for (int i = _snake.Count - 1; i > 0; i--)
            {
                ObjectOnMap snakePart = new ObjectOnMap(_snake[i - 1].X, _snake[i - 1].Y, TypeOfField.Snake);
                _snake[i] = snakePart;
            }
            _snake[0] = snakeHead;

            _addSnakeToMap();
        }

        public GameLogic()
        {
            _snake = new List<ObjectOnMap>();
            Map = new TypeOfField[SIZE, SIZE];
            SnakeDirection = Direction.Up;
            ObjectOnMap snakeHead = new ObjectOnMap(10, 10, TypeOfField.Snake);
            _snake.Add(snakeHead);
            _addSnakeToMap();
            _generateRandomApple();
        }

        private void _addToMap(ObjectOnMap objectOnMap)
        {
            Map[objectOnMap.X, objectOnMap.Y] = objectOnMap.TypeOfField;
        }

        private void _looseGame()
        {
            GameOverEvent?.Invoke();
        }

        private void _clearSnakeFromMap()
        {
            foreach (ObjectOnMap objectOnMap in _snake)
            {
                Map[objectOnMap.X, objectOnMap.Y] = TypeOfField.Empty;
            }
        }

        private void _addSnakeToMap()
        {
            foreach (ObjectOnMap objectOnMap in _snake)
            {
                Map[objectOnMap.X, objectOnMap.Y] = TypeOfField.Snake;
            }
        }

        private void _generateRandomApple()
        {
            List<Point> coordinatesOfAllEmptyPlaces = new List<Point>();

            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    if(Map[i, j] == TypeOfField.Empty)
                        coordinatesOfAllEmptyPlaces.Add(new Point(i, j));
                }

            Random random = new Random();
            int index = random.Next(coordinatesOfAllEmptyPlaces.Count);
            Point selectedRandomLocation = coordinatesOfAllEmptyPlaces[index];

            _apple = new ObjectOnMap(selectedRandomLocation.X,
                selectedRandomLocation.Y,
                TypeOfField.Apple);

            Map[selectedRandomLocation.X,
                selectedRandomLocation.Y] = TypeOfField.Apple;
        }

        private void _pointScored()
        {
            _generateRandomApple();
            _score++;
            ScoredPointEvent?.Invoke();
        }

        private struct ObjectOnMap
        {
            public int X;
            public int Y;
            public TypeOfField TypeOfField;

            public ObjectOnMap(int x, int y, TypeOfField typeOfField)
            {
                X = x;
                Y = y;
                TypeOfField = typeOfField;
            }
        }
    }
}