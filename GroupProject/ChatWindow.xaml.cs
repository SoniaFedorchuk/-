using BLL;
using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private static string remote_ip_address = "192.168.1.141";
        private static int remote_port = 1488;
        UdpClient client = new UdpClient(0);
        IBLLClass _bll = null;
        UserDTO user = null;

        public ChatWindow(IBLLClass _bll, UserDTO user)
        {
            InitializeComponent();

            UserControlReceivedMessage receivedMessage = new UserControlReceivedMessage("blyat cyka");

            chat_panel.Children.Add(new UserControlReceivedMessage("blyat cyka"));
            chat_panel.Children.Add(new UserControlSentMessage("blyat cyka"));

            this._bll = _bll;
            this.user = user;

            switch ((UserRole)user.RoleId)
            {
                case UserRole.Librarian:
                    SendMessage("<connect-lib>");
                    break;
                case UserRole.User:
                    SendMessage("<connect-user>");
                    break;
            }

            Task.Run(() => Listen());
        }

        private void Listen()
        {
            IPEndPoint ipEndPoint = null;
            while (true)
            {
                try
                {
                    byte[] data = client.Receive(ref ipEndPoint);

                    string message = Encoding.UTF8.GetString(data);

                    switch ((UserRole)user.RoleId)
                    {
                        case UserRole.Librarian:
                            switch (message)
                            {
                                //case "<connect>":
                                //    SendMessage("<lib-login=\"\">")
                                //default:
                                //    break;
                            }
                            break;
                        case UserRole.User:
                            break;
                    }


                    Dispatcher.Invoke(() =>
                    {

                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Woops, something went wrong\n{ex.Message}");
                }
            }
        }

        private void SendMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(remote_ip_address), remote_port);

                byte[] data = Encoding.UTF8.GetBytes(message);
                client.Send(data, data.Length, ipEndPoint);
            }
        }

        private void Close_Connections(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch ((UserRole)user.RoleId)
            {
                case UserRole.Librarian:
                    SendMessage("<leave-lib>");
                    break;
                case UserRole.User:
                    SendMessage("<leave-user>");
                    break;
            }
        }
    }
}
