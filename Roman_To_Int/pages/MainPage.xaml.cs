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

namespace Roman_To_Int
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            RomanTextBox.Text = "";

        }




        private int RomanToInt(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();

            dic.Add('I', 1);
            dic.Add('V', 5);
            dic.Add('X', 10);
            dic.Add('L', 50);
            dic.Add('C', 100);
            dic.Add('D', 500);
            dic.Add('M', 1000);


            int number = 0;

            for (int i = 0; i < s.Length; ++i)
            {
                number += dic[s[i]];
            }



            if (s.Contains("IV") || s.Contains("IX"))
                number -= 2;

            if (s.Contains("XL") || s.Contains("XC"))
                number -= 20;

            if (s.Contains("CD") || s.Contains("CM"))
                number -= 200;


            return number;

        }



        private static string IntToRoman(int number)
        {



            if ((number < 0) || (number > 3999))
                return "";

            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + IntToRoman(number - 1000);
            if (number >= 900) return "CM" + IntToRoman(number - 900);
            if (number >= 500) return "D" + IntToRoman(number - 500);
            if (number >= 400) return "CD" + IntToRoman(number - 400);
            if (number >= 100) return "C" + IntToRoman(number - 100);
            if (number >= 90) return "XC" + IntToRoman(number - 90);
            if (number >= 50) return "L" + IntToRoman(number - 50);
            if (number >= 40) return "XL" + IntToRoman(number - 40);
            if (number >= 10) return "X" + IntToRoman(number - 10);
            if (number >= 9) return "IX" + IntToRoman(number - 9);
            if (number >= 5) return "V" + IntToRoman(number - 5);
            if (number >= 4) return "IV" + IntToRoman(number - 4);
            if (number >= 1) return "I" + IntToRoman(number - 1);

            throw new ArgumentOutOfRangeException("something bad happened");


        }






        private void IButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += "I";
        }
        private void VButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'V';
        }
        private void XButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'X';
        }
        private void LButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'L';
        }
        private void CButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'C';
        }
        private void DButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'D';
        }
        private void MButton_MouseDown(object sender, RoutedEventArgs e)
        {
            RomanTextBox.Text += 'M';
        }



        private void RomanTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ArabTextBox.Text = (RomanToInt(RomanTextBox.Text)).ToString();
        }

        private void ArabTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                RomanTextBox.Text = IntToRoman(System.Convert.ToInt16(ArabTextBox.Text));
            }
            catch (Exception ex)
            {
                ArabTextBox.Text = "0";

            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("BallPage.xaml", UriKind.Relative));
        }



    }
}
