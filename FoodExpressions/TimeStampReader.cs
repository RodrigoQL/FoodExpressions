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
        private static string fileName = "timestamps.txt";
        private static string directory = Directory.GetParent( Environment.CurrentDirectory ).ToString();
        private static string path = Path.GetFullPath( Path.Combine( directory, @"..\..\..\" ) );
        
        public static Dictionary<string,int> GetTimeStamps(string candidate)
        {
            Dictionary<string, int> timestamps = new Dictionary<string, int>();
            timestamps.Add( "sampleContempt", 0 );

            string fileString = File.ReadAllLines( buildPath( candidate ) )[0];
            Dictionary<string,double[]> dictionary = JsonConvert.DeserializeObject
                ( fileString, typeof(Dictionary<string,double[]>) ) as Dictionary<string, double[]>;
            foreach(var item in dictionary)
            {
                timestamps.Add( item.Key, ( int )item.Value[0] );
            }

            return timestamps;
        } 
        
        private static string buildPath(string candidate)
        {
            return path + "\\" + candidate + "\\" + fileName;
        }
    }
}
