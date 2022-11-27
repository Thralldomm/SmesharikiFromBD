using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Roman_To_Int
{
    public class BallInfo : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int age;
        private string zodiac_Sign;
        private byte[] image;

        public int Id
        {
            get { return id; }
            set
            {
                id= value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Zodiac_Sign
        {
            get { return zodiac_Sign; }
            set
            {
                zodiac_Sign = value;
                OnPropertyChanged("Zodiac_Sign");
            }
        }

        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
