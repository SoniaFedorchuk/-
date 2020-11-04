using BLL;
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
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        private int _index;
        IBLLClass _bll;
        public SellWindow(int index, int maximum, IBLLClass bll)
        {
            InitializeComponent();
            amount.Maximum = maximum;
            amount.IsSnapToTickEnabled = true;
            _index = index;
            _bll = bll;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_bll.SellBooks(_index, (int)amount.Value))
                MessageBox.Show("Succesfully selled");
            this.Close();
        }

    }
}
