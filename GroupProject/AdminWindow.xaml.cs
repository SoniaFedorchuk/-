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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        IBLLClass _bll = null;

        public AdminWindow(IBLLClass _bll)
        {
            InitializeComponent();

            this._bll = _bll;
            UsersData.IsReadOnly = true;

            this.Update();
        }

        public void Update()
        {
            UsersData.ItemsSource = _bll.GetAllUsers();
        }

        private void ChangeRole(object sender, RoutedEventArgs e)
        {
            ChangeRoleWindow changeRoleWindow =  new ChangeRoleWindow(((UserDTO)UsersData.SelectedItem).Id, _bll);
            changeRoleWindow.Owner = this;
            changeRoleWindow.Show();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            _bll.DeleteUser(((UserDTO)UsersData.SelectedItem).Id);
            this.Update();
        }
    }
}
