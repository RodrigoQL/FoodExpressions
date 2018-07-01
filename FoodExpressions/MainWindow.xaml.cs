using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodExpressions
{

    public partial class MainWindow : Window
    {
        private Dictionary<string, int> emoteDictionary;
        private string person;
        private int frame;
        private string currentEmotion = "sampleNormal";
        private int frameModifier = 0;
        public bool imageExists = false;
        public MainWindow()
        {
            InitializeComponent();
            TrainSampleGenerator.GenerateBase();
            person = "022";
            ShowPerson();
        }

        private void ShowPerson()
        {
            personLabel.Content = person;
            currentEmotion = "sampleNormal";
            emoteDictionary = TimeStampReader.GetTimeStamps( person );
            frameModifier = 0;
            DisplayEmotion();
            /*
            BitmapImage image = ImageDisplay.GetImageFromTrain( person, currentEmotion );
            if ( image != null)
            {
                imageExists = true;
                Display.Source = image;
            }
            else
            {
                imageExists = false;
                Display.Source = ImageDisplay.GetImage( person, emoteDictionary[currentEmotion] );
            }
            
            imageModifier = 0;
            frame = emoteDictionary[currentEmotion] + imageModifier;
            frameLabel.Content = frame;
            */
        }
        private void DisplayEmotion(object sender = null, RoutedEventArgs e = null)
        {
            if (sender != null)
            { 
                currentEmotion = ( sender as Button ).Name.ToString();
                frameModifier = 0;
            }
            frame = emoteDictionary[currentEmotion] + frameModifier;
            imageExists = ImageDisplay.ImageExists( person, currentEmotion, frame );
            if (imageExists)
            {
                removeSample.Visibility = Visibility.Visible;
                addSample.Visibility = Visibility.Hidden;
            }
            else
            {
                removeSample.Visibility = Visibility.Hidden;
                addSample.Visibility = Visibility.Visible;
            }
            Display.Source = ImageDisplay.GetImage( person, frame );
            frameLabel.Content = frame;
        }
        private void NumberBuilder(int addition)
        {
            int actual = Int32.Parse( person );
            actual += addition;
            if (actual < 100)
            {
                if (actual < 10)
                {
                    person = "00" + actual.ToString();
                }
                else
                {
                    person = "0" + actual.ToString();
                }
            }
            else
            {
                person = actual.ToString();
            }
        }
        public void NextPerson(object sender, RoutedEventArgs e)
        {
            NumberBuilder( +1 );
            ShowPerson();
        }
        public void PrevPerson(object sender, RoutedEventArgs e)
        {
            NumberBuilder( -1 );
            ShowPerson();
        }
        public void NextFrame(object sender, RoutedEventArgs e)
        {
            frameModifier += 5;
            DisplayEmotion();
        }
        public void PrevFrame(object sender, RoutedEventArgs e)
        {
            frameModifier -= 5;
            DisplayEmotion();
        }
        public void AddSample(object sender, RoutedEventArgs e)
        {
            TrainSampleGenerator.AddImage( person, currentEmotion, frame );

            emoteDictionary = TimeStampReader.GetTimeStamps( person );
            frameModifier = 0;
            DisplayEmotion( );
        }
        public void RemoveSample(object sender, RoutedEventArgs e)
        {
            TrainSampleGenerator.RemoveImage( person, currentEmotion, frame );

            emoteDictionary = TimeStampReader.GetTimeStamps( person );
            frameModifier = 0;
            DisplayEmotion( );
        }
    }
}
