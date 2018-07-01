using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FoodExpressions
{
    public class ImageDisplay
    {
        private static string directory = Directory.GetParent( Environment.CurrentDirectory ).ToString();
        private static string path = Path.GetFullPath( Path.Combine( directory, @"..\..\..\" ) );

        public static BitmapImage GetImage(string candidate, int image)
        {
            DirectoryInfo taskDirectory = new DirectoryInfo( buildPath( candidate ) );
            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( image.ToString()+"_" ) );
            var a = taskFiles.ToArray()[0];

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri( a.FullName );
            logo.EndInit();
            return logo;

        }
        private static string buildPath(string candidate)
        {
            return path + "\\" + candidate + "\\frontal\\";
        }
    }
}
