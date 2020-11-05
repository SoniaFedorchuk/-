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
    /// Interaction logic for ChangeRoleWindow.xaml
    /// </summary>
    public partial class ChangeRoleWindow : Window
    {
        IBLLClass _bll = null;
        int user_id;
        public ChangeRoleWindow(int user_id, IBLLClass bll)
        {
            InitializeComponent();

            this._bll = bll;
            this.user_id = user_id;

            roleComboBox.ItemsSource = _bll.GetAllRoles();
            roleComboBox.DisplayMemberPath = nameof(RoleDTO.Name);
        }

        private void ChangeRole(object sender, RoutedEventArgs e)
        {
            _bll.ChangeUserRole(user_id, roleComboBox.SelectedIndex + 1);

            MessageBox.Show("Role succesfully changed");
            ((AdminWindow)this.Owner).Update();
            this.Close();
        }
    }
}
