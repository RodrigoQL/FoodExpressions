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
        private static Dictionary<string, int> emotionDictioary;
        public static BitmapImage GetImage(string candidate, int image)
        {
            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.FrontalSamplesPath( candidate ) );
            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( image.ToString()+"_" ) );
            var first = taskFiles.ToArray()[0];

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri( first.FullName );
            logo.EndInit();
            return logo;
        }

        public static Dictionary<string, int> GetEmotionDictionary()
        {
            if(emotionDictioary == null)
            {
                emotionDictioary = new Dictionary<string, int>();
                emotionDictioary["sampleAngry"] = 1;
                emotionDictioary["sampleContempt"] = 2;
                emotionDictioary["sampleDisgust"] = 3;
                emotionDictioary["sampleFear"] = 4;
                emotionDictioary["sampleHappy"] = 5;
                emotionDictioary["sampleSad"] = 6;
                emotionDictioary["sampleSurprise"] = 7;
                emotionDictioary["sampleNormal"] = 8;
            }
            return emotionDictioary;
        }
    }
}
