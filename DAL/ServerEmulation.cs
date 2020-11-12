using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    class ServerEmulation
    {
        private const int port = 1488;
        private static Dictionary<IPEndPoint, bool> librarians = new Dictionary<IPEndPoint, bool>();
        private static Dictionary<IPEndPoint, IPEndPoint> users = new Dictionary<IPEndPoint, IPEndPoint>();

        static void Main(string[] args)
        {
            UdpClient server = new UdpClient(port);
            IPEndPoint ip_end_point = null;

            try
            {
                while (true)
                {
                    byte[] data = server.Receive(ref ip_end_point);

                    string message = Encoding.UTF8.GetString(data);

                    switch (message)
                    {
                        case "<connect-lib>":
                            if (!librarians.ContainsKey(ip_end_point))
                                librarians.Add(ip_end_point, true);
                            break;
                        case "<connect-user>":
                            if (!users.ContainsKey(ip_end_point))
                            {
                                IPEndPoint lib_ip_point = librarians.FirstOrDefault(l => l.Value == true).Key;
                                if (lib_ip_point == null)
                                    SendMessage("<all-busy>", ip_end_point, server);
                                else
                                {
                                    users.Add(ip_end_point, lib_ip_point);
                                    SendMessage("<connect>", lib_ip_point, server);
                                    SendMessage("<connect>", ip_end_point, server);
                                }
                            }
                            break;
                        case "<pause-lib>":
                            if (librarians.ContainsKey(ip_end_point))
                                librarians[ip_end_point] = false;
                            break;
                        case "<resume-lib>":
                            if (librarians.ContainsKey(ip_end_point))
                                librarians[ip_end_point] = true;
                            break;
                        case "<leave-lib>":
                            if (librarians.ContainsKey(ip_end_point))
                                librarians.Remove(ip_end_point);
                            break;
                        case "<leave-user>":
                            if (users.ContainsKey(ip_end_point))
                                users.Remove(ip_end_point);
                            break;
                        default:
                            if(Regex.Match(message, "<user-login=\".+\">").Success)
                            {
                                SendMessage(data, users[ip_end_point], server);
                            }
                            if (Regex.Match(message, "<lib-login=\".+\">").Success)
                            {
                                SendMessage(data, users.FirstOrDefault(u => u.Value == ip_end_point).Key, server);
                            }
                            //Match match = Regex.Match(message, "\"");
                            //message.Substring(match.Index, match.NextMatch().Index);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private static void SendMessage(string message, IPEndPoint ip_end_point, UdpClient server)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            server.Send(data, data.Length, ip_end_point);
        }
        private static void SendMessage(byte[] data, IPEndPoint ip_end_point, UdpClient server)
        {
            server.Send(data, data.Length, ip_end_point);
        }
    }
}
