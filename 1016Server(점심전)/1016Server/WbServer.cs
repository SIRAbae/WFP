using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016Server
{
    class WbServer
    {
        private Socket server;
        private Form1 form;

        public string ServerIp { get; private set; }
        public int ServerPort  { get; private set;  }

        public void ParentInfo(Form1 f)
        {
            form = f;
        }

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

                Thread th = new Thread(new ParameterizedThreadStart(ServerThread));
                th.Start(this);

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void ServerThread(object data)
        {
            while (true)
            {
                Socket client = server.Accept();
                
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                String temp = String.Format("[클라이언트접속]{0}:{1} 성공",
                      ip.Address, ip.Port);
                form.LogMessage(temp);

                
                byte[] msg = new byte[1024];
                //수신
                if (client.Receive(msg) != 0)   // 수신한 문자열이 있으면 화면에 출력
                {
                    form.ShortMessage(Encoding.Default.GetString(msg));
                }
                else
                {
                    break;
                }

                client.Send(msg, msg.Length, SocketFlags.None); // 문자열 전송
                

                client.Close();         //  소켓 연결 끊기

                String temp1 = String.Format("[클라이언트해제]{0}:{1} 성공",
                      ip.Address, ip.Port);
                form.LogMessage(temp1);
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
