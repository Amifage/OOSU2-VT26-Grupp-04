using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Medlem_Presentationslager
{
    public class ByteToImageConverter : IValueConverter // class som omvandlar byte till en bild
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                var image = new BitmapImage();
                using (var mem = new System.IO.MemoryStream(bytes))
                {
                    image.BeginInit(); //startar processen
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat; // är för att bilden inte ska ändras från urstprung som användaren lägger in
                    image.CacheOption = BitmapCacheOption.OnLoad; //laddar in bilden till RAM-minnet
                    image.StreamSource = mem;
                    image.EndInit(); // stänger proseccen 
                }
                return image; //skickar bilden till fönstret
            }
            // Standardbild om profilbild saknas
            return new BitmapImage(new Uri("pack://application:,,,/Gemini_Generated_Image_rtnq6xrtnq6xrtnq.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }
}