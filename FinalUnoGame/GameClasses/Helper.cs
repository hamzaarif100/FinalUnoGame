using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//This is the required import
using System.Windows.Media.Imaging;
namespace FinalUnoGame
{
    public static class Helper
    {
        public static BitmapImage GetImage(string path)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            img.EndInit();

            return img;
        }
    }
}
