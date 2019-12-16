using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Application = Microsoft.Office.Interop.Excel.Application;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace _1002_shop_program
{
    public partial class Form1 : Form
    {

        #region API
        BuyDateSearcher bd = new BuyDateSearcher();
        BlogDateSearcher blogd = new BlogDateSearcher();
        #endregion



        #region 컬렉션
        List<BuyDate> buylist = new List<BuyDate>();
        List<BlogDate> bloglist = new List<BlogDate>();
        #endregion

        #region 속성 및 프로퍼티
        Form2 form2;
        int y = 0; //상품리스트 좌표
        int x = 0; //상품리스트 좌표
        int searchNum; //검색번호  
        #endregion

        #region 생성자
        public Form1()
        {
            InitializeComponent();
            comboBox2.Text = "관련도";

        }
        #endregion

        #region 상품검색
        //검색
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                searchNum = 1;
                textBox2.Text = searchNum.ToString();
                bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
                button1.Enabled = true;
                button3.Enabled = true;
                PlintSearchList();
            }
            catch (Exception)
            {
                MessageBox.Show("검색할 수 없습니다.");
            }
        }

        //다음>>
        private void button1_Click(object sender, EventArgs e)
        {
            searchNum++;
            textBox2.Text = searchNum.ToString();
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
            PlintSearchList();
        }

        //<<이전
        private void button3_Click(object sender, EventArgs e)
        {
            if (searchNum <= 1)
                return;

            searchNum--;
            textBox2.Text = searchNum.ToString();
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
            PlintSearchList();
        }

        //출력
        private void PlintSearchList()
        {
            panel1.Controls.Clear();
            x = 0;
            y = 0;

            Loading lo = new Loading();

            lo.Function = (() =>
            {
                for (int i = 0; i < bd.buydateList.Count; i++)
                {
                    string filepath = bd.buydateList[i].Image;
                    byte[] data = new System.Net.WebClient().DownloadData(filepath);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
                    Image img = Image.FromStream(ms);

                    shopingItem item =
                        new shopingItem(img, bd.buydateList[i].Title,
                                        bd.buydateList[i].Lprice.ToString(),
                                        bd.buydateList[i].Link.ToString(),
                                        this);

                    this.Invoke(new MethodInvoker(
                         delegate ()
                         {
                             panel1.Controls.Add(item);
                         }
                     )
                    );

                    if (x == 0 && y == 0)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                item.Location = new System.Drawing.Point(x, y + 10);
                            }
                          )
                        );
                        x = item.Location.X;
                        y = item.Location.Y;
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                item.Location = new System.Drawing.Point(x, y + 150);
                            }
                          )
                        );
                        x = item.Location.X;
                        y = item.Location.Y;
                    }

                    item.num.Text = (i + 1).ToString();
                }
            });
            lo.ShowDialog();
        }

        Loading lo = new Loading();


        //상세검색(shopingItem 에서 호출)
        public void CallSearchItemModal(string name)
        {
            //검색 모달 호출
            form2 = new Form2(name);
            form2.Show();
        }
        #endregion

        #region 정보출력
        private void PrintBuylist()
        {
            foreach (BuyDate bu in buylist)
            {
                listBox1.Items.Add(bu.Title);
            }
        }
        #endregion

        #region 장바구니

        //장바구니 추가(shopingitem에서 호출)
        public void AddList(int idx)
        {
            buylist.Add(bd.buydateList[idx - 1]);
            listBox1.Items.Clear();
            PrintBuylist();
            AllMoney();
        }
        //장바구니 물건 삭제
        private void button2_Click(object sender, EventArgs e)
        {
            if (buylist.Count <= 0 || listBox1.SelectedIndex == -1)
                return;

            int a = listBox1.SelectedIndices.Count;
            BuyDate[] buys = new BuyDate[a];

            for (int i = 0; i < a; i++)
            {
                buys[i] = buylist[listBox1.SelectedIndices[i]];
            }

            for (int i = 0; i < a; i++)
            {
                buylist.Remove(buys[i]);
            }

            listBox1.Items.Clear();
            foreach (BuyDate bu in buylist)
            {
                listBox1.Items.Add(bu.Title);
            }
            AllMoney();
        }

        //장바구니 전체 돈 출력
        private void AllMoney()
        {
            int all = 0;
            foreach (BuyDate bu in buylist)
            {
                all += bu.Lprice;
            }
            textBox7.Text = all.ToString();
        }

        //버튼_txt저장
        private void button4_Click(object sender, EventArgs e)
        {
            exelSave();
            #region 메모장 저장
            //string path =
            //    @"C:\Users\USER\Desktop\이것저것\text.txt";
            //string save = string.Empty;
            //save += "합계 :" + textBox7.Text +"원"+ "\r\n"+
            //"===============================" + "\r\n";
            //foreach (BuyDate bu in buylist)
            //{
            //    save += "상품명 :" + bu.Title + "\r\n" +
            //            "URL :" + bu.Link + "\r\n" +
            //            "최고가 :" + bu.Hprice + "\r\n" +
            //            "최저가 :" + bu.Lprice + "\r\n" +
            //            "===============================" + "\r\n";
            //}
            //System.IO.File.WriteAllText(path,save, Encoding.Default);
            #endregion
            MessageBox.Show("저장완료");
        }
        #region exel 저장
        private void exelSave()
        {

            lo.ShowDialog();
            Application app = new Application();
            Workbook workbook = app.Workbooks.Add();
            workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\하기싫어\test2.xlsx");

            workbook = app.Workbooks.Open(Filename: @"C:\Users\USER\Desktop\하기싫어\test2.xlsx");
            Worksheet worksheet = workbook.Worksheets.Add();

            int r = 1;
            foreach (BuyDate d in buylist)
            {
                worksheet.Cells[r, 1] = d.Title;
                worksheet.Cells[r, 2] = d.Lprice;
                worksheet.Cells[r, 3] = d.Link;
                r++;
            }
            workbook.Save();
            //workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");
            workbook.Close();


        }
        #endregion

        #endregion


        #region exel불러오기
        private void button5_Click(object sender, EventArgs e)
        {

        }


        public void ReadExcel(string path)
        { // path는 Excel파일의 전체 경로입니다.
          // 예. D:\test\test.xslx
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            string str1;
            int num1;
            string str3;
            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(Filename: path);
                // path 대신 문자열도 가능합니다
                // 예. Open(@"D:\test\test.xslx");
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                // 첫번째 Worksheet를 선택합니다.
                Excel.Range rng = ws.UsedRange;   // '여기'
                                                  // 현재 Worksheet에서 사용된 셀 전체를 선택합니다.
                object[,] data = rng.Value;
                // 열들에 들어있는 Data를 배열 (One-based array)로 받아옵니다. // -->, i = 0
                for (int r = 1; r <= data.GetLength(0); r++)
                {
                    for (int c = 1; c <= data.GetLength(1); c++)
                    {
                        if (data[r, c] == null)
                        {
                            continue;
                        }
                        else
                        {
                            if (c == 1)
                            {
                                str1 = (string)(rng.Cells[r, c] as Excel.Range).Value2; // 열과 행에 해당하는 데이터를 문자열로 반환
                                MessageBox.Show(str1);


                            }
                            else if (c == 2)
                            {
                                num1 = (int)(rng.Cells[r, c] as Excel.Range).Value2; // 열과 행에 해당하는 데이터를 문자열로 반환
                                string str2 = string.Format("{0}", num1);
                                MessageBox.Show(str2);
                            }
                            else
                            {
                                str3 = (string)(rng.Cells[r, c] as Excel.Range).Value2; // 열과 행에 해당하는 데이터를 문자열로 반환
                                MessageBox.Show(str3);
                            }
                            // Data 빼오기
                            // data[r, c] 는 excel의 (r, c) 셀 입니다.
                            // data.GetLength(0)은 엑셀에서 사용되는 행의 수를 가져오는 것이고,
                            // data.GetLength(1)은 엑셀에서 사용되는 열의 수를 가져오는 것입니다.
                            // GetLength와 [ r, c] 의 순서를 바꿔서 사용할 수 있습니다.

                            wb.Close(true);
                            excelApp.Quit();
                        }


                    }

                   // BuyDate buy = new BuyDate(string t, string l, string i, int lp, int hp, int pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
        }
        static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion
    }
}
