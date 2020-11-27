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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for UserControlSentMessage.xaml
    /// </summary>
    public partial class UserControlSentMessage : UserControl
    {
        public UserControlSentMessage(string text)
        {
            InitializeComponent();
            text_block.Text = text;
            date_block.Text = DateTime.Now.ToShortDateString();
        }
    }
}
