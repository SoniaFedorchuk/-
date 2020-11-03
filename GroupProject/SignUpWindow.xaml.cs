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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private IBLLClass _bll = null;

        public SignUpWindow()
        {
            InitializeComponent();
            _bll = new BLLClass();

            roleComboBox.ItemsSource = _bll.GetAllRoles();
            roleComboBox.DisplayMemberPath = nameof(RoleDTO.Name);
        }

        private void SingUpButtonClick(object sender, RoutedEventArgs e)
        {
            if (_bll.IsExistsUserByLogin(signUpLogin.Text))
            {
                MessageBox.Show("This login is unavailable");
            }

            _bll.AddUser(new UserDTO()
            {
                Login = signUpLogin.Text,
                Password = signUpPassword.Text
            });
        }
    }
}
