using BLL;
using BLL.DTO;
using DAL.Models;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignWindow : Window
    {
        private IBLLClass _bll = null;

        public SignWindow()
        {
            InitializeComponent();

            _bll = new BLLClass();
        }

        private void SingInButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_bll.IsExistsUserByLogin(signInLogin.Text))
            {
                MessageBox.Show("This login wasn't registered");
                return;
            }

            if (_bll.IsExistsUserByLogin(signInLogin.Text) && !_bll.IsExistsUserByLoginAndPassword(signInLogin.Text, signInPassword.Text))
            {
                MessageBox.Show("Incorrect password");
                return;
            }

            UserDTO user = _bll.GetUserByLoginAndPassword(signInLogin.Text, signInPassword.Text);

            switch ((UserRole)user.RoleId)
            {
                case UserRole.Admin:
                    new AdminWindow(_bll).Show();
                    this.Close();
                    break;
                case UserRole.Librarian:
                    new LibrarianWindow(_bll, user).Show();
                    this.Close();
                    break;
                case UserRole.User:
                    new ShopWindow(_bll, user).Show();
                    this.Close();
                    break;
                default:
                    break;
            }

        }

        private void SingUpButtonClick(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(_bll);
            signUpWindow.Show();
            this.Close();
        }
    }
}
