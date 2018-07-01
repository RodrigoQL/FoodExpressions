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
        public static Dictionary<string,int> GetTimeStamps(string candidate)
        {
            Dictionary<string, int> timestamps = new Dictionary<string, int>();
            
            // INITIAL TIME STAMPS
            timestamps.Add( "sampleContempt", 0 );

            string fileString = File.ReadAllLines( PathHelper.TimeStampsPath( candidate ) )[0];
            Dictionary<string,double[]> dictionary = JsonConvert.DeserializeObject
                ( fileString, typeof(Dictionary<string,double[]>) ) as Dictionary<string, double[]>;
            foreach(var item in dictionary)
            {
                timestamps.Add( item.Key, ( int )item.Value[0] );
            }

            // NEW TIME STAMPS


            return timestamps;
        } 
    }
}
