using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodExpressions
{
    public class TimeStampReader
    {
        public static Dictionary<string,int> GetTimeStamps(string person)
        {
            Dictionary<string, int> timestamps = new Dictionary<string, int>();
            if (!File.Exists( PathHelper.TimeStampsPath( person ) ))
            {
                return null;
            }
            // INITIAL TIME STAMPS
            timestamps.Add( "sampleContempt", 0 );
            
            string fileString = File.ReadAllLines( PathHelper.TimeStampsPath( person ) )[0];
            Dictionary<string, double[]> dictionary = JsonConvert.DeserializeObject
                ( fileString, typeof( Dictionary<string, double[]> ) ) as Dictionary<string, double[]>;
            foreach (var item in dictionary)
            {
                timestamps.Add( item.Key, ( int )item.Value[0] );
            }

            // NEW TIME STAMPS
            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.PersonTrainPath( person ) );
            FileInfo[] files = taskDirectory.GetFiles( "*.jpeg" );
            Dictionary<string, int> emotionDictionary = ImageDisplay.GetEmotionDictionary();
            foreach (FileInfo file in files)
            {
                string[] emotion_frame = file.Name.Split( '_' );
                string emotion = emotionDictionary.FirstOrDefault(
                    x => x.Value == Int32.Parse( emotion_frame[0] ) )
                    .Key;
                timestamps[emotion] = Int32.Parse( emotion_frame[1].Replace( ".jpeg", "" ) );
            }

            return timestamps;
        } 
    }
}
