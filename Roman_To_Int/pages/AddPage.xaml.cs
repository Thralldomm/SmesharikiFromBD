using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO; 
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        BallInfoes ball = new BallInfoes();

        MainWindow mainWindow;


        public AddPage(MainWindow main)
        {
            InitializeComponent();

            mainWindow = main;
             
            MainGrid.DataContext = ball;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDialogButton_Click(object sender, RoutedEventArgs e)
        { 
            Stream myStream;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                if ((myStream = dlg.OpenFile()) != null)
                {
                    string strfilename = dlg.FileName;
                    string filetext = File.ReadAllText(strfilename);

                    dlg.DefaultExt = ".png";
                    dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    dlg.Title = "Open Image";
                    dlg.InitialDirectory = @"C:\Users\123\";

                    BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                    myImage.Source = image;
                     
                }
                myStream.Dispose();

            }
        }

         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder(); //хранит ошибки
            errors = null;

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                errors.AppendLine("Insert a Name");
            if(string.IsNullOrWhiteSpace(Zodiac_SignTextBox.Text))
                errors.AppendLine("Insert a Zodiac_Sign");
            if(Int32.Parse(AgeTextBox.Text) <0 || Int32.Parse(AgeTextBox.Text) > 100 || string.IsNullOrWhiteSpace(AgeTextBox.Text))
                errors.AppendLine("Insert a correct age");
            

            if (errors != null)
            { 
                MessageBox.Show(errors.ToString());
            }
            else
            {
                using(DataModel dm = new DataModel())
                { 

                    ball.Name = NameTextBox.Text;
                    ball.Age = Int32.Parse(AgeTextBox.Text);
                    ball.Zodiac_Sign = Zodiac_SignTextBox.Text;
                    ball.Image = myImage.Source.ToString();


                    try
                    {
                        dm.BallInfoes.Add(ball);
                        dm.SaveChanges();
                    }catch(Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}");
                    }
                   
                     
                }
                 
                mainWindow.frame.Navigate(new BallPage(mainWindow));

            }

 
        }
          
    }
}
