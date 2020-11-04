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
    public partial class EditBookWindow : Window
    {
        IBLLClass _bll;
        BookDTO updatable_book;

        public EditBookWindow(BookDTO book,IBLLClass bll)
        {
            InitializeComponent();
            _bll = bll;

            txtName.Text = book.Name;
            txtPublisher.Text = book.Publisher;
            txtPages.Text = book.Pages.ToString();
            txtPrice.Text = book.Price.ToString();
            txtAmount.Text = book.Amount.ToString();
            txtAuthor.Text = book.Author;
            txtGenre.Text = book.Genre;
            Date.SelectedDate = book.Year;

            updatable_book = book;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Enter name of the book!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPublisher.Text))
            {
                MessageBox.Show("Enter publisher of the book!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Enter price of the book!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPages.Text))
            {
                MessageBox.Show("Enter pages of the book!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Enter amount of the books");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Enter pages of the book!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Enter amount of the book!");
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Price is a number!");
                return;
            }
            if (!decimal.TryParse(txtPages.Text, out _))
            {
                MessageBox.Show("Pages is a number!");
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Amount is a number!");
                return;
            }

            updatable_book.Price = decimal.Parse(txtPrice.Text);
            updatable_book.Pages = int.Parse(txtPages.Text);
            updatable_book.Amount = int.Parse(txtAmount.Text);
            updatable_book.Year = Date.SelectedDate.Value;
            updatable_book.Author = txtAuthor.Text;
            updatable_book.Genre = txtGenre.Text;            
            updatable_book.Name = txtName.Text;
            updatable_book.Publisher = txtPublisher.Text;

            _bll.UpdateBook(updatable_book);
            MessageBox.Show("Succesfully edited");
            this.Close();
        }
    }
}
