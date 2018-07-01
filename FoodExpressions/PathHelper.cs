using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodExpressions
{
    public class PathHelper
    {
        private static string DIRECTORY = Directory.GetParent( Environment.CurrentDirectory ).ToString();
        private static string PATH = Path.GetFullPath( Path.Combine( DIRECTORY, @"..\..\..\" ) );

        public static string TimeStampsPath(string person)
        {
            return PATH + "\\Food\\" + person + "\\timestamps.txt";
        }

        public static string FrontalSamplesPath(string person)
        {
            return PATH + "\\Food\\" + person + "\\frontal\\";
        }

        public static string TrainPath()
        {
            return PATH + "\\Train\\";
        }

        public static string PersonTrainPath(string person)
        {
            return PATH + "\\Train\\" + person + "\\";
        }

        public static string FoodPath()
        {
            return PATH + "\\Food\\";
        }
    }
}
