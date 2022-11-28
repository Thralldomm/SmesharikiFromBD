

##ListView

```
<ListView Grid.Row ="0"
	  x:Name="ListView"
	  ScrollViewer.HorizontalScrollBarVisibility="Disabled"HorizontalContentAlignment="Center" >
	  
            <ListView.ItemsPanel>
                <ItemsPanelTemplate>
                    <WrapPanel Orientation="Horizontal" HorizontalAlignment="Center"/>
                </ItemsPanelTemplate>
            </ListView.ItemsPanel>
	    
            <ListView.ItemTemplate>
                <DataTemplate>
                    <Grid>
                        <Grid.RowDefinitions>
                            <RowDefinition/>
                            <RowDefinition/>
                            <RowDefinition/>
                        </Grid.RowDefinitions>
			
                        <Image Grid.Row="2"
			       HorizontalAlignment="Center"
			       Height="100"
			       Width="100">

                                <Image.Source>

                                <Binding Path="Password" >
   
                                    <Binding.TargetNullValue>
                                        <ImageSource>products/tire_0.jpg</ImageSource>
                                    </Binding.TargetNullValue>
                                    
                                </Binding>
                            </Image.Source>
                        </Image>
                        
                        <TextBlock Text="{Binding Login}"
				   VerticalAlignment="Center"
				   TextAlignment="Center"
				   TextWrapping="Wrap"
				   HorizontalAlignment="Center"
				   Margin="5 5"
                                   FontSize="10"
				   Grid.Row="0"/>
                        <TextBlock Text="{Binding Password, StringFormat={}{0}}"
				   VerticalAlignment="Center"
				   TextAlignment="Center"
				   TextWrapping="Wrap"
				   HorizontalAlignment="Center"
				   Margin="5 5"
                                   FontSize="10" Grid.Row="1"/>     
                    </Grid>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>

```
 


##DataGrid

```

<Grid Background="Pink"  >

        <Grid.RowDefinitions>
            <RowDefinition Height="300"/>
            <RowDefinition Height="70"/>
        </Grid.RowDefinitions>

        <DataGrid
              
            Background="Pink"
            Grid.Row="0"
            AutoGenerateColumns="False"
             x:Name="MainGrid"
            IsReadOnly="True"
           
            >

            <DataGrid.Columns>


                <DataGridTextColumn  Binding="{Binding Name}" Header="Name" />

                <DataGridTextColumn Binding="{Binding Age}" Header="Age"/>

                <DataGridTextColumn   Binding="{Binding Zodiac_Sign}" Header="Zodiac_Sign" />

                
                <DataGridTemplateColumn Width="auto" >
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Button Content="Update" Name="Edit_Button" Click="Edit_Button_Click" Visibility="Visible" BorderBrush="AliceBlue"/>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>


                <DataGridTemplateColumn Header="Фото" IsReadOnly="True" >
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Image Source="{Binding Image}"
                                   Height="100"
                                   Width="100" 
                                    />
                            
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>

                
                
            </DataGrid.Columns>
            

        </DataGrid>

        <Button Content="Add" Grid.Row="1" Click="AddButton_Click" Margin="200 0 0 10" VerticalAlignment="Bottom" HorizontalAlignment="Left" Width="100" Name="AddButton"/>
        <Button Content="Delete" Grid.Row="1" Click="DeleteButton_Click" Margin="0 0 200 10" VerticalAlignment="Bottom" HorizontalAlignment="Right" Width="100" Name="DeleteButton"/>

       <!-- <Image x:Name="myImage" Height="100" Width="100"/>  -->
    </Grid>

```



##D - Delete

```

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

```


### Error

####the object cannot be deleted because it was not found in the objectstatemanager

Attach Before Deleting

```

  var selectedUsers = ListView.SelectedItems.Cast<BallInfoes>().ToList();

             
			//для списка:
                        foreach(var user in selectedUsers)
                        {
                            dm.BallInfoes.Attach(user);
                        }


			//для одного объекта:
			dm.BallInfoes.Attach(user);

                        


                        dm.BallInfoes.RemoveRange(selectedUsers);
                        dm.SaveChanges();

```



##C - Create



```

 public partial class AddPage : Page
    {
        BallInfoes ball = new BallInfoes();


        public AddPage()
        {
            InitializeComponent();
             
            MainGrid.DataContext = ball;
        }


```



```

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

```

### Saving the object

```

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



            if(errors != null)
            { 
                MessageBox.Show(errors.ToString());
            }
            else
            {
                using(DataModel dm = new DataModel())
                {
                    BallInfoes ball = new BallInfoes();

                    ball.Name = NameTextBox.Text;
                    ball.Age = Int32.Parse(AgeTextBox.Text);
                    ball.Zodiac_Sign = Zodiac_SignTextBox.Text;
                    ball.Image = myImage.Source.ToString();

                    dm.BallInfoes.Add(ball);
                    dm.SaveChanges();

                }


            }



        }


```



##Возникло исключение stackoverflow 

В MainWindow создавался экземпляр страницы BallPage, в BallPage создавался экземпляр MainWindow. Получился бесконечный цикл

Вместо того, чтобы создавать экземпляр MainWindow, просто передадим ссылку на него в конструктор BallPagе.



####MainWindow:
```
 public MainWindow()
        {
            InitializeComponent();

            frame.Navigate(new BallPage(this)); // открытие страницы
             
        }
```



####BallPage:
```
 private MainWindow mainWindow;
         

        public BallPage(MainWindow mainWindow)
        {
            InitializeComponent();
 
            this.mainWindow = mainWindow;

            using (DataModel dm = new DataModel())
            {
                ListView.ItemsSource = dm.BallInfoes.ToList();
                 
            }



        }

```




##U - Update

Используем тот же AddPage, немного его модифицировав. Теперь в его конструктор принимаем объект.
А в случае добавления передаем null


```

 BallInfoes ball = new BallInfoes();


        public UpdatePage(BallInfoes bally)
        {
            InitializeComponent();

            ball = null;

            if (bally != null)
                ball = bally;

            MainGrid.DataContext = ball;
        }
```


```
 using(DataModel dm = new DataModel())
                { 
                    ball.Name = NameTextBox.Text;
                    ball.Age = Int32.Parse(AgeTextBox.Text);
                    ball.Zodiac_Sign = Zodiac_SignTextBox.Text;
                    ball.Image = myImage.Source.ToString();

                    dm.BallInfoes.AddOrUpdate(ball);  //добавить или изменить
                    dm.SaveChanges();

                }
``` 




## Поиск, Сортировка, Фильтрация

### Задать начальные значения

```

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

```


### Сортировка

```
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
```


### Поиск

```
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
```


### Фильтрация

```

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

```







