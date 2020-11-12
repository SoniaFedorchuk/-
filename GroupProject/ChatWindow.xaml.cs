using BLL;
using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
        private static int remote_port = 9999;
        UdpClient client = new UdpClient(0);
        IBLLClass _bll = null;
        UserDTO user = null;

        public ChatWindow(IBLLClass _bll, UserDTO user)
        {
            InitializeComponent();

            this._bll = _bll;
            this.user = user;

            switch ((UserRole)user.RoleId)
            {
                case UserRole.Librarian:
                    SendMessage("<connect-lib>");
                    break;
                case UserRole.User:
                    SendMessage("<connect-user>");
                    foreach (Control item in grid.Children.OfType<Control>())
                    {
                        if (item.Tag != null && item.Tag.ToString() == "for-lib")
                            item.Visibility = Visibility.Collapsed;
                    }
                    break;
            }

            Task.Run(() => Listen());
        }

        private void Listen()
        {
            IPEndPoint ipEndPoint = null;
            while (true)
            {
                //try
                //{
                        byte[] data = client.Receive(ref ipEndPoint);

                        string message = Encoding.UTF8.GetString(data);

                        switch ((UserRole)user.RoleId)
                        {
                            case UserRole.Librarian:
                                switch (message)
                                {
                                    case "<connect>":
                                        SendMessage($"<lib-login=\"{user.Login}\">");
                                        break;
                                    default:
                                        if (Regex.Match(message, "<user-login=\".+\">").Success)
                                        {
                                            Match match = Regex.Match(message, "\"");
                                            user_text_block.Text = message.Substring(match.Index, -(match.Index - match.NextMatch().Index));
                                        }
                                        else
                                        {
                                            Dispatcher.Invoke(() =>
                                            {
                                                UserControlReceivedMessage receivedMessage = new UserControlReceivedMessage(message);
                                                receivedMessage.HorizontalAlignment = HorizontalAlignment.Left;
                                                receivedMessage.VerticalAlignment = VerticalAlignment.Bottom;
                                                chat_panel.Children.Add(receivedMessage);
                                            });
                                        }
                                        break;
                                }
                                break;
                            case UserRole.User:
                                switch (message)
                                {
                                    case "<connect>":
                                        SendMessage($"<user-login=\"{user.Login}\">");
                                        break;
                                    case "<all-busy>":
                                        MessageBox.Show("Sorry but all librarians are busy now");
                                        this.Close();
                                        break;
                                    default:
                                        if (Regex.Match(message, "<lib-login=\".+\">").Success)
                                        {
                                            Match match = Regex.Match(message, "\"");
                                            user_text_block.Text = message.Substring(match.Index, -(match.Index - match.NextMatch().Index));
                                        }
                                        else
                                        {
                                            Dispatcher.Invoke(() =>
                                            {
                                                UserControlReceivedMessage receivedMessage = new UserControlReceivedMessage(message);
                                                receivedMessage.HorizontalAlignment = HorizontalAlignment.Left;
                                                receivedMessage.VerticalAlignment = VerticalAlignment.Bottom;
                                                chat_panel.Children.Add(receivedMessage);
                                            });
                                        }
                                        break;
                                }
                                break;
                        }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"Woops, something went wrong\n{ex.Message}");
                //}
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

        private void Get_Offline(object sender, RoutedEventArgs e)
        {
            SendMessage("<pause-lib>");
        }

        private void Get_Online(object sender, RoutedEventArgs e)
        {
            SendMessage("<resume-lib>");
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sended_message.Text))
            {
                SendMessage(sended_message.Text);

                UserControlSentMessage sentMessage = new UserControlSentMessage(sended_message.Text);
                sentMessage.HorizontalAlignment = HorizontalAlignment.Right;
                sentMessage.VerticalAlignment = VerticalAlignment.Bottom;
                chat_panel.Children.Add(sentMessage);
                sended_message.Text = string.Empty;
            }
        }
    }
}
