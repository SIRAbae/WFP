using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016Server
{
    public partial class Form1 : Form
    {
        WbServer server = new WbServer();

        public Form1()
        {
            InitializeComponent();
            server.ParentInfo(this);        //<===============
        }

        #region  소켓에서 보낸 정보
        public void LogMessage(string str)
        {
            str += "(" + DateTime.Now.ToString() + ")";
            Ui.LogPrint(listBox1, str);
        }

        public void ShortMessage(string str)
        {
            str += "(" + DateTime.Now.ToString() + ")";
            Ui.LogPrint(listBox2, str);
        }
        #endregion

        #region 메뉴

        private void 프로그램종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 서버연결CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerInfo dlg = new ServerInfo();
            if(dlg.ShowDialog()== DialogResult.OK)
            {
                if (server.ServerRun(dlg.Port) == true)
                {
                    Ui.FillDrawing(panel1, Color.Blue);
                    Ui.LabelState(label1, true);

                    String temp = String.Format("[서버실행]{0}:{1} 성공",
                        server.ServerIp, server.ServerPort);
                    Ui.LogPrint(listBox1, temp);
                }
                else
                {
                    String temp = String.Format("[서버실행]{0}:{1} 실패",
                        server.ServerIp, server.ServerPort);
                    Ui.LogPrint(listBox1, temp);
                }
            }            
        }

        private void 서버연결해제DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.ServerStop();

            Ui.FillDrawing(panel1, Color.Red);
            Ui.LabelState(label1, false);

            String temp = String.Format("[서버종료]{0}:{1} 성공",
                        server.ServerIp, server.ServerPort);
            Ui.LogPrint(listBox1, temp);
        }
        #endregion
    }
}
