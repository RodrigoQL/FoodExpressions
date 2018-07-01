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
        public static BitmapImage GetImage(string person, int image)
        {
            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.FrontalSamplesPath( person ) );
            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( image.ToString()+"_" ) );
            if(taskFiles.Count() == 0)
            {
                return null;
            }
            var first = taskFiles.ToArray()[0];

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri( first.FullName );
            logo.EndInit();
            return logo;
        }

        public static BitmapImage GetImageFromTrain(string person, string emotion)
        {
            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.PersonTrainPath( person ) );
            if(emotionDictioary == null)
            {
                GetEmotionDictionary();
            }
            int number = emotionDictioary[emotion];
            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( number.ToString() + "_" ) );
            if(taskFiles.Count() > 0)
            {
                var first = taskFiles.ToArray()[0];

                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri( first.FullName );
                logo.EndInit();
                return logo;
            }
            else
            {
                return null;
            }
        }
        
        public static bool ImageExists(string person, string emotion, int frame)
        {
            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.PersonTrainPath( person ) );
            if (emotionDictioary == null)
            {
                GetEmotionDictionary();
            }
            int numberEmotion = emotionDictioary[emotion];
            string fileName = numberEmotion + "_" + frame;
            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( fileName ) );
            return taskFiles.Count() > 0;
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
