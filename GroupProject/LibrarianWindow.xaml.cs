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
        private UserDTO user = null;
        

        public LibrarianWindow()
        {
            InitializeComponent();
            _bll = new BLLClass();
            this.Update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddBookWindow(_bll).Show();

            this.Update();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select book that you want to delete");
                return;
            }

            _bll.DeleteBook((dataGrid.SelectedItem as BookDTO).Id);

            this.Update();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditBookWindow editBook = new EditBookWindow((dataGrid.SelectedItem as BookDTO), _bll);
            editBook.Show();
            this.Update();
        }

        private void Update()
        {
            this.dataGrid.ItemsSource = _bll.GetAllBooks();
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select book that you want to sell");
                return;
            }

            SellWindow sell_book = new SellWindow((dataGrid.SelectedItem as BookDTO).Id, (dataGrid.SelectedItem as BookDTO).Amount, _bll);
            sell_book.Show();
        }
    }
}
