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
    /// Interaction logic for BuyBookWindow.xaml
    /// </summary>
    public partial class BuyBookWindow : Window
    {
        BookDTO book = null;
        IBLLClass _bll = null;
        public BuyBookWindow(BookDTO bookDTO, IBLLClass _bll)
        {
            InitializeComponent();

            book = bookDTO;
            this._bll = _bll;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Name : {bookDTO.Name}\n\n");
            stringBuilder.Append($"Publisher : {bookDTO.Publisher}\n\n");
            stringBuilder.Append($"Pages : {bookDTO.Pages}\n\n");
            stringBuilder.Append($"Year : {bookDTO.Year}\n\n");
            stringBuilder.Append($"Author : {bookDTO.Author}\n\n");
            stringBuilder.Append($"Genre : {bookDTO.Genre}");

            txtInfo.Text = stringBuilder.ToString();

            amount.Value = 1;
            amount.Minimum = 1;
            amount.Maximum = bookDTO.Amount;
        }

        private void AmountChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblPrice.Content = book.Price * (decimal)amount.Value;
        }

        private void BuyBook(object sender, RoutedEventArgs e)
        {
            _bll.SellBooks(book.Id, (int)amount.Value);

            MessageBox.Show("You successfully buy book)))");
            this.Close();
        }
    }
}
