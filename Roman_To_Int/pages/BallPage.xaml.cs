using Microsoft.SqlServer.Server;
using Roman_To_Int.pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Roman_To_Int
{ 
     
    /// <summary>
    /// Логика взаимодействия для BallPage.xaml
    /// </summary>
    public partial class BallPage : Page
    {

        private MainWindow mainWindow;
         

        public BallPage(MainWindow mainWindow)
        {
            InitializeComponent();

 
            this.mainWindow = mainWindow;


            using (DataModel db = new DataModel())
            {
                var currentProducts = db.BallInfoes.ToList();
                ListView.ItemsSource = currentProducts;

                CountBlock.Text = $"Количество: {currentProducts.Count} из {db.BallInfoes.ToList().Count}";
                 


                List<String> l = new List<String>() { "Name", "Descending", "Ascending" };

                SortComboBox.ItemsSource = l;
                SortComboBox.SelectedIndex = 0;



                var allNames = db.BallInfoes.Select(p => p.Name).Distinct().ToList();

                allNames.Insert(0, "Names");


                FilterComboBox.ItemsSource = allNames;
                FilterComboBox.SelectedIndex = 0;

                 
            }

        }

        
  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }

         
        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        { 
            mainWindow.frame.Navigate(new UpdatePage((sender as Button).DataContext as BallInfoes, mainWindow)); 
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new AddPage(mainWindow));
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            using (DataModel dm = new DataModel())
            {

                var selectedUsers = ListView.SelectedItems.Cast<BallInfoes>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить {selectedUsers.Count()} объектов", "Внимание!",
                   MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
 

                    try
                    {
                        foreach(var user in selectedUsers)
                        {
                            dm.BallInfoes.Attach(user);
                        }
                        
                        dm.BallInfoes.RemoveRange(selectedUsers);
                        dm.SaveChanges();
                        ListView.ItemsSource = dm.BallInfoes.ToList();
                        MessageBox.Show("Пользователи удалены!");
                         
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}");
                    }
                

                }
            }

             
        }

          

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataModel dm = new DataModel();
            var currentProducts = dm.BallInfoes.ToList();

             
            // Сортировка
            if (SortComboBox.SelectedIndex > 0)
            {

                if (SortComboBox.SelectedItem == "Ascending")
                {
                    currentProducts = currentProducts.OrderBy(p => p.Name).ToList();
                }

                if (SortComboBox.SelectedItem == "Descending")
                {
                    currentProducts = currentProducts.OrderByDescending(p => p.Name).ToList();
                }


                ListView.ItemsSource = currentProducts;
            }
        }

         

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            using (DataModel db = new DataModel())
            {
                // Поиск
                if (SearchTextBox.Text != "")
                {
                    var FoundBalls = db.BallInfoes.Where(p => p.Name.Contains(SearchTextBox.Text) || p.Zodiac_Sign.Contains(SearchTextBox.Text)).ToList();  // p.Age.Contains(Int32.Parse(SearchTextBox.Text))  
                    ListView.ItemsSource = FoundBalls;
                };

            }

        }



        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataModel dm = new DataModel();
            var currentProducts = dm.BallInfoes.ToList();

            // Фильтрация


            if (FilterComboBox.SelectedValue == "Names")
                ListView.ItemsSource = currentProducts;

            if (FilterComboBox.SelectedValue != null && FilterComboBox.SelectedValue != "Names")
            {
                currentProducts = currentProducts.Where(p => p.Name.Trim() == FilterComboBox.SelectedValue.ToString()).ToList();
                ListView.ItemsSource = currentProducts;
 
            }


        }
    }
}