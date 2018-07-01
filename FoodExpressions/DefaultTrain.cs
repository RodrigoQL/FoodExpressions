using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodExpressions
{
    public class DefaultTrain
    {
        public static void Generate()
        {
            if (!Directory.Exists( PathHelper.TrainPath() ))
            {
                Directory.CreateDirectory( PathHelper.TrainPath() );
            }
            string[] directories = Directory.GetDirectories( PathHelper.FoodPath() );
            foreach (string directory in directories)
            {
                string person = directory.Substring( directory.Length - 3, 3 );
                if (!Directory.Exists( PathHelper.PersonTrainPath( person ) ))
                {
                    Directory.CreateDirectory( PathHelper.PersonTrainPath( person ) );
                    Dictionary<string, int> timestamps = TimeStampReader.GetTimeStamps( person );

                    Dictionary<string, int> emotions = ImageDisplay.GetEmotionDictionary();
                    foreach(var item in timestamps)
                    {
                        if (item.Key.StartsWith( "sample" )) {
                            DirectoryInfo taskDirectory = new DirectoryInfo( PathHelper.FrontalSamplesPath( person ) );
                            var taskFiles = taskDirectory.GetFiles( "*.jpeg" ).Where( p => p.Name.StartsWith( item.Value + "_" ) );
                            if (taskFiles.Count() > 0)
                            {
                                var first = taskFiles.ToArray()[0];
                                File.Copy( first.FullName, PathHelper.PersonTrainPath( person ) + emotions[item.Key] + "_" + item.Value + ".jpeg" );
                            }
                        }
                    }


                }
            }
        }
    }
}
