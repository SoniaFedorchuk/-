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
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private IBLLClass _bll = null;
        public ShopWindow()
        {
            InitializeComponent();
            _bll = new BLLClass();
            this.Update();
        }

        private void btnHints_Checked(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }
        private void Update()
        {
            this.dataGrid.ItemsSource = _bll.GetAllBooks();
        }

        private void Finder(object sender, RoutedEventArgs e)
        {
            //List<BookDTO> bookDTOs = new List<BookDTO>();
            // bookDTOs = bookDTOs.FindAll(x => x.Name.Contains(txtFind.Text));

        }
    }
}

