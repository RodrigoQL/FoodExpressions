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
        private string currentEmotion = "sampleNormal";
        private int imageModifier = 0;
        public MainWindow()
        {
            InitializeComponent();
            DefaultTrain.Generate();
            person = "022";
            ShowPerson();
        }

        private void ShowPerson()
        {
            emoteDictionary = TimeStampReader.GetTimeStamps( person );
            Display.Source = ImageDisplay.GetImage( person, emoteDictionary["sampleNormal"]  );
            imageModifier = 0;
        }
        private void DisplayEmotion(object sender = null, RoutedEventArgs e = null)
        {
            if (sender != null)
            { 
                currentEmotion = ( sender as Button ).Name.ToString();
                imageModifier = 0;
            }
            Display.Source = ImageDisplay.GetImage( person, emoteDictionary[currentEmotion] + imageModifier );
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
            imageModifier += 5;
            DisplayEmotion();
        }
        public void PrevFrame(object sender, RoutedEventArgs e)
        {
            imageModifier -= 5;
            DisplayEmotion();
        }
    }
}
