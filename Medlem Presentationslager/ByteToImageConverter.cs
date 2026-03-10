using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Medlem_Presentationslager
{
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                var image = new BitmapImage();
                using (var mem = new System.IO.MemoryStream(bytes))
                {
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                return image;
            }
            // Standardbild om profilbild saknas
            return new BitmapImage(new Uri("pack://application:,,,/Gemini_Generated_Image_rtnq6xrtnq6xrtnq.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }
}