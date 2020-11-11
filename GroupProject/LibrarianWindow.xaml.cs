using BLL;
using BLL.DTO;
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
using System.Windows.Shapes;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for LibrarianWindow.xaml
    /// </summary>
    public partial class LibrarianWindow : Window
    {
        private IBLLClass _bll = null;
        

        public LibrarianWindow()
        {
            InitializeComponent();
            _bll = new BLLClass();

            this.Update();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            new AddBookWindow(_bll).Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select book that you want to delete");
                return;
            }

            _bll.DeleteBook((dataGrid.SelectedItem as BookDTO).Id);

            this.Update();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            EditBookWindow editBook = new EditBookWindow((dataGrid.SelectedItem as BookDTO), _bll);
            editBook.Show();
        }

        private void Sell(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select book that you want to sell");
                return;
            }

            SellWindow sell_book = new SellWindow((dataGrid.SelectedItem as BookDTO).Id, (dataGrid.SelectedItem as BookDTO).Amount, _bll);
            sell_book.Show();
        }

        private void UpdateEvent(object sender, RoutedEventArgs e)
        {
            this.Update();
        }

        public void Update()
        {
            this.dataGrid.ItemsSource = _bll.GetAllBooks();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            _bll.AddBooks(new BookDTO()
            {
                Name = "Eugene Onegin",
                Publisher = "mem",
                Pages = 100,
                Price = 60,
                Year = DateTime.Parse("09/10/1873"),
                Amount = 100,
                Author = "Pushkin",
                Genre = "Drama",
            });
            _bll.AddBooks(new BookDTO()
            {
                Name = "The Captain’s Daughter",
                Publisher = "mem",
                Pages = 300,
                Price = 80,
                Year = DateTime.Parse("09/07/1860"),
                Amount = 100,
                Author = "Julien",
                Genre = "Drama"
            });
            _bll.AddBooks(new BookDTO()
            {
                Name = "Anna Karenina",
                Publisher = "nemem",
                Pages = 170,
                Price = 60,
                Year = DateTime.Parse("06/15/1855"),
                Amount = 100,
                Author = "Pushkin",
                Genre = "Drama"
            });
            _bll.AddBooks(new BookDTO()
            {
                Name = "What I Believe",
                Publisher = "nemem",
                Pages = 100,
                Price = 70,
                Year = DateTime.Parse("06/15/1855"),
                Amount = 100,
                Author = "Rokfeler",
                Genre = "Sientific"
            });
            _bll.AddBooks(new BookDTO()
            {
                Name = "Harry Potter and the Philosopher's Stone",
                Publisher = "nemem",
                Pages = 230,
                Price = 50,
                Year = DateTime.Parse("05/21/1999"),
                Amount = 100,
                Author = "Rowling",
                Genre = "Fantasy"
            });
        }
    }
}
