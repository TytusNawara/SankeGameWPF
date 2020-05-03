using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeZadanieRekrutacyjne
{
    public class RectangleDisplayer
    {
        private Rectangle orginalRectangle;
        private Canvas canvas;
        public static readonly int InitialSize = 18;
        private int _x;
        private int _y;
        public int X
        {
            get => _x;
            set
            {
                _x = value; 
                Canvas.SetLeft(orginalRectangle, _x - orginalRectangle.Width/2);
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                Canvas.SetTop(orginalRectangle, _y - orginalRectangle.Height/2);
            }
        }

        public SolidColorBrush Color
        {
            set => orginalRectangle.Fill = value;
        }

        public RectangleDisplayer(Canvas canvas, int x, int y)
        {
            this.canvas = canvas;
            orginalRectangle = new Rectangle();
            orginalRectangle.Fill = Brushes.Blue;
            orginalRectangle.Width = InitialSize;
            orginalRectangle.Height = InitialSize;
            X = x;
            Y = y;
            canvas.Children.Add(orginalRectangle);
        }
    }
}