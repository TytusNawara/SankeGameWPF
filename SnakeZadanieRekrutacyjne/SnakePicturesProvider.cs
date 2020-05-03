using System;
using System.Windows.Media.Imaging;

namespace SnakeZadanieRekrutacyjne
{
    public class SnakePicturesProvider
    {
        private string[] URLs = { "https://media.gettyimages.com/photos/venomous-snake-picture-id157479804?s=2048x2048",
            "https://media.gettyimages.com/photos/closeup-of-snake-with-mouth-open-picture-id615323829?s=2048x2048",
            "https://media.gettyimages.com/photos/mexican-black-kingsnake-studio-shot-picture-id460703771?s=2048x2048",
            "https://media.gettyimages.com/photos/venomous-bush-viper-snake-showing-aggression-picture-id624458526?s=2048x2048",
            "https://media.gettyimages.com/photos/grass-snake-picture-id624626136?s=2048x2048"
        };

        private BitmapImage[] snakePictures;

        public SnakePicturesProvider()
        {
            snakePictures = new BitmapImage[URLs.Length];
        }

        public BitmapImage ProvideSnakePicture()
        {
            int randomIndex = _randomIndexFromSnakePicturesArray();
            BitmapImage toReturn = snakePictures[randomIndex];
            if (toReturn != null)
                return toReturn;

            ImageDownloader imageDownloader = new ImageDownloader(URLs[randomIndex]);
            return imageDownloader.DowloadImage();
        }

        private int _randomIndexFromSnakePicturesArray()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, snakePictures.Length);
            return randomIndex;
        }
    }
}