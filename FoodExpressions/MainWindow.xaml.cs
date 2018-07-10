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
        public bool validPerson = false;
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
            emotionLabel.Content = currentEmotion.Replace( "sample", "" );
            CleanWindow();
            sampleNormal.Background = Brushes.LightSkyBlue;
            emoteDictionary = TimeStampReader.GetTimeStamps( person );
            frameModifier = 0;
            if (emoteDictionary == null)
            {
                personLabel.Content = person + " NOPERSON";
                frameLabel.Content = "NOPERSON";
                Display.Source = null;
                validPerson = false;
            }
            else
            {
                validPerson = true;
                DisplayEmotion();
            }
        }
        private void DisplayEmotion(object sender = null, RoutedEventArgs e = null)
        {
            if (!validPerson)
            {
                return;
            }
            
            if (sender != null)
            {
                Button button = sender as Button;
                CleanWindow();
                button.Background = Brushes.LightSkyBlue; ;
                currentEmotion = ( button ).Name.ToString();
                emotionLabel.Content = currentEmotion.Replace( "sample", "" );
                frameModifier = 0;
            }

            if (!emoteDictionary.ContainsKey( currentEmotion ))
            {
                frame = frameModifier;
            }
            else
            {
                frame = emoteDictionary[currentEmotion] + frameModifier;
            }
            
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
        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Left )
            {
                PrevFrame( sender, e );
                return;
            }
            if(e.Key == Key.Right)
            {
                NextFrame( sender, e );
                return;
            }
            if(e.Key== Key.Up)
            {
                NextPerson(sender, e);
                return;
            }
            if (e.Key == Key.Down)
            {
                PrevPerson( sender, e );
            }
        }
        private void CleanWindow()
        {
            sampleAngry.Background = Brushes.LightGray;
            sampleSad.Background = Brushes.LightGray;
            sampleHappy.Background = Brushes.LightGray;
            sampleNormal.Background = Brushes.LightGray;
            sampleFear.Background = Brushes.LightGray;
            sampleDisgust.Background = Brushes.LightGray;
            sampleSurprise.Background = Brushes.LightGray;
            sampleContempt.Background = Brushes.LightGray;
        }
    }
}
