using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;


namespace _1002
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class Form1 : Form 
    {

        wbClient client = new wbClient();
        StationSearcher s = StationSearcher.Instance;
        HtmlDocument hdoc;
        public List<ChargeStation> timest = new List<ChargeStation>();
        Timer timer = new System.Windows.Forms.Timer();

        string IP = "192.168.0.80";
        string Port = "9000";

        public Form1()
        {
            InitializeComponent();
            client.ParentInfo(this);
        }





        #region 핸들러
        private void Form1_Load(object sender, EventArgs e)
        {
            web_map.ObjectForScripting = this;
            hdoc = web_map.Document;
            if (client.Connet(IP, Port) == true)
            {
                MessageBox.Show("연결성공");
            }
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
                return;
            PrintInfo();
        }

       

        public void CheckMarker()
        {
            List<string> ar = new List<string>();
            foreach (ChargeStation ls in s)
            {
                listBox2.Items.Clear();
                object[] ps = null;
                if (checkBox2.Checked == true)
                {
                    if (ls.ChgerType == 03 || ls.ChgerType == 06 || ls.ChgerType == 07)
                    {
                        int stat = CheckState(ls);
                        ps = new object[] { ls.Lat, ls.Lng, stat }; ;
                        if (stat == 2)
                            ar.Add(ls.StatNm);
                        hdoc.InvokeScript("MakeMarker", ps);
                    }
                }
                else if (checkBox3.Checked == true)
                {
                    if (ls.ChgerType == 01 || ls.ChgerType == 03 || ls.ChgerType == 05 || ls.ChgerType == 06)
                    {
                        int stat = CheckState(ls);
                        ps = new object[] { ls.Lat, ls.Lng, stat }; ;
                        if(stat==2)
                            ar.Add(ls.StatNm);
                        hdoc.InvokeScript("MakeMarker", ps);
                    }

                }
                else if (checkBox4.Checked == true)
                {
                    if (ls.ChgerType == 03 || ls.ChgerType == 05 || ls.ChgerType == 06)
                    {
                        int stat = CheckState(ls);
                        ps = new object[] { ls.Lat, ls.Lng, stat }; ;
                        if (stat == 2)
                            ar.Add(ls.StatNm);
                        hdoc.InvokeScript("MakeMarker", ps);
                    }
                }
                else
                    return;

                ar = ar.Distinct().ToList();
                foreach (string name in ar)
                {
                    listBox2.Items.Add(name);
                }
            }
        }

      
        #endregion

        #region Form1 내부 메서드
        private string whattype(ChargeStation data)
        {
            switch (data.ChgerType)
            {
                case 01: return string.Format("DC차데모");
                case 02: return string.Format("AC완속");
                case 03: return string.Format("DC차데모+AC3상");
                case 04: return string.Format("DC콤보");
                case 05: return string.Format("DC차데모+DC콤보");
                case 06: return string.Format("DC차데모+AC3상+DC콤보");
                case 07: return string.Format("AC3상");
            }
            return "없음";
        }
        private string whatstat(ChargeStation data)
        {
            switch (data.Stat)
            {
                case 1: return string.Format("통신이상");
                case 2: return string.Format("충전대기");
                case 3: return string.Format("충전중");
                case 4: return string.Format("운영중지");
                case 5: return string.Format("점검중");

            }
            return "없음";
        }

        private int CheckState(ChargeStation ls)
        {
            foreach (ChargeStation ch in s)
            {
                if (ls.StatId.Equals(ch.StatId))
                {
                    if (ch.Stat == 2)
                        return 2;
                }
            }
            return ls.Stat;
        }

        private bool isEqual(double n1, double n2)
        {
            const double Eps = 0.0000000005;
            return (Math.Abs(n1 - n2) < Eps);
        }

        private void PrintInfo()
        {
            listView1.Items.Clear();
            ChargeStation data = null;
            foreach (ChargeStation d in s)
            {
                string place = (string)listBox2.SelectedItem;
                if (d.StatNm.Equals(place))
                {
                    data = d;
                    string id = string.Format("{0}", d.ChgerId);
                    string type = whattype(d);
                    string stat = whatstat(d);
                    string[] arr = { id, type, stat };
                    ListViewItem a = new ListViewItem(arr);
                    listView1.Items.Add(a);
                }
            }

            textBox1.Text = data.AddrDoro;
            textBox2.Text = data.UseTime;

            object[] ps = new object[] { data.Lat, data.Lng };
            MarkerInfo(data.Lat, data.Lng);
            hdoc.InvokeScript("setCenter", ps);

        }

        public void Atimer(ChargeStation ch)
        {
            timer.Interval = 1000; // 1초
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            GetTime();
        }

        private void GetTime()
        {
            listBox1.Items.Clear();
            var stattimes = timest.Select(x => x.StatTime).ToArray();
            var chgerid = timest.Select(x => x.ChgerId).ToArray();
            
            for(int i=0;i< stattimes.Length;i++)
            {
                string temp = stattimes[i];
                int temp1 = chgerid[i];

                DateTime start = Convert.ToDateTime(temp);
                DateTime endline = start + new TimeSpan(1, 30, 0);
                DateTime now = DateTime.Now;
                TimeSpan endtime = endline - now;
                if (endtime.Seconds < 0)
                {
                    endtime = new TimeSpan(0, 0, 0);
                }
                listBox1.Items.Add(string.Format("[충전기 ID :{0}]", temp1));
                listBox1.Items.Add(string.Format("예상대기시간 :{0}:{1}:{2}", endtime.Hours, endtime.Minutes, endtime.Seconds));
                listBox1.Items.Add(string.Format("예상완료시간 : {0}", endline));
                listBox1.Items.Add("--------------------------------------------------------------");
            }
        }

        #endregion

        #region JS호출 메서드

        public void SendToServer(object swLat, object swLng, object neLat, object neLng)
        {
            double swlat = (double)swLat;
            double swlng = (double)swLng;
            double nelat = (double)neLat;
            double nelng = (double)neLng;
            client.Packging(swlat, swlng, nelat, nelng);
        }
        
        public void MarkerInfo(object lat,object lng)
        {
            timer.Dispose();
            timest.Clear();
            listView1.Items.Clear();
            listBox1.Items.Clear();
            foreach (ChargeStation ch in s)
            {
                if (isEqual(ch.Lat,(double)lat) && isEqual(ch.Lng, (double)lng))
                {

                    string id = string.Format("{0}", ch.ChgerId);
                    string type = whattype(ch);
                    string stat = whatstat(ch);
                    string[] arr = { id, type, stat };
                    ListViewItem a = new ListViewItem(arr);
                    listView1.Items.Add(a);

                    textBox1.Text = ch.AddrDoro;
                    textBox2.Text = ch.UseTime;

                    if(ch.StatTime !="")
                    {
                        timest.Add(ch);
                        GetTime();
                        Atimer(ch);
                    }
                    
                }
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string addr = string.Format("'{0}'", textBox3.Text);
            object[] ps = new object[] { textBox3.Text };
            hdoc.InvokeScript("Serch", ps);
        }

    }

   
        
}


