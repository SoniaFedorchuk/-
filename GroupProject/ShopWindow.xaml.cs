using BLL;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private IBLLClass _bll = null;
        ObservableCollection<Books> books = new ObservableCollection<Books>();
        
        public ShopWindow()
        {
            InitializeComponent();
            _bll = new BLLClass();

            for (int i = 0; i < (_bll.GetAllBooks()).ToList().Count; ++i)
            {
                books.Add(new Books() { Author = ((_bll.GetAllBooks()).ToList()[i].Author), Name = ((_bll.GetAllBooks()).ToList()[i].Name) });
            }
            listBox.ItemsSource = books;
            
        }

        private void btnHints_Checked(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }
        public class Books
        {
            private string name;
            public string Name
            {
                get { return name; }
                set 
                {
                    name = value;
                    OnPropertyChanged();
                }
            }

            private string author;
            public string Author
            {
                get { return author; }
                set
                {
                    author = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
