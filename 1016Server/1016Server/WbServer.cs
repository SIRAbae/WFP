using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016Server
{
    class WbServer
    {
        private Socket server;

        public string ServerIp { get; private set; }
        public int ServerPort  { get; private set;  }

        public bool ServerRun(int port)
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork,
                                           SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                server.Bind(ipep);
                server.Listen(20);

                IPEndPoint ip = (IPEndPoint)server.LocalEndPoint;
                ServerIp = ip.Address.ToString();
                ServerPort = port;

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void ServerStop()
        {
            try
            {
                server.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
