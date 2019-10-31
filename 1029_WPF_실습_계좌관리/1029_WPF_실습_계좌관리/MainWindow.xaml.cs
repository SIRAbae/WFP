using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1029_WPF_실습_계좌관리
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        AccList acclist = null;
        AccIoList acciolist = null;
        public MainWindow()
        {
            InitializeComponent();
            acclist = (AccList)FindResource("acclist"); //-> 해줘야함
            acciolist = (AccIoList)FindResource("acciolist");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(name.Text == "" || id.Text == "")
            {
                MessageBox.Show("계좌ID와 이름은 필수 입력입니다.");
                return;
            }
            Account acc = new Account()
            {
                Id = int.Parse(id.Text),
                Name = name.Text,
                Bal = int.Parse(bal.Text),
                Date = DateTime.Now
            };

            acclist.Add(acc);

            MessageBox.Show("계좌가 생성되었습니다.");
        }

        private void RemoveButton_Click_1(object sender, RoutedEventArgs e)
        {

            AccList acclist = (AccList)FindResource("acclist");


            AccIoList acciolist = (AccIoList)FindResource("acciolist");

            int idx = 0;
            while (idx < acciolist.Count)
            {
                AccIo acio = acciolist[idx];
                if (acio.Id.Equals(acclist[listbox1.SelectedIndex].Id))
                {
                    acciolist.RemoveAt(idx);
                    //acciolist.Remove(aio);
                }
                else
                    ++idx;
            }

            if (listbox1.SelectedIndex >= 0)
            {
                acclist.RemoveAt(listbox1.SelectedIndex);

            }

            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));
            Account acc = (Account)view.CurrentItem;

            if (id.Text != "")
            {
                acc.Id = int.Parse(id.Text);
                id.Clear();
            }
            else
            {
                MessageBox.Show("수정할 ID를 입력하세요");
                return;
            }
        }

        private void Moneyout_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));
            Account acc = (Account)view.CurrentItem;        //리스트에서 찾은거를 객체화해줌.

            if (int.Parse(money.Text) <= 0)
            {
                MessageBox.Show("0원이하 출금불가");
                return;
            }
            else if(int.Parse(money.Text) > acc.Bal)
            {
                MessageBox.Show("잔액보다 높은금액 출금불가");
                return;
            }
            acc.Bal -= int.Parse(money.Text);



            AccIo accio = new AccIo()
            {
                Id = int.Parse(accioid.Text),
                History = Moneyin.Content.ToString(),
                Money = int.Parse(money.Text),
                Balance = int.Parse(acciobal.Text),
                Date = DateTime.Now
            };

            acciolist.Add(accio);
            //listbox2.Items.Add(accio);
        }

        private void Moneyin_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));
            Account acc = (Account)view.CurrentItem;        //리스트에서 찾은거를 객체화해줌.

            if (int.Parse(money.Text) <= 0)
            {
                MessageBox.Show("0원이하 입금불가");
                return;
            }
            acc.Bal += int.Parse(money.Text);




            AccIo accio = new AccIo()
            {
                Id = int.Parse(accioid.Text),
                History = Moneyin.Content.ToString(),
                Money = int.Parse(money.Text),
                Balance = int.Parse(acciobal.Text),
                Date = DateTime.Now
            };

            acciolist.Add(accio);
            //listbox2.Items.Add(accio);
            //foreach (Account a in acclist)
            //{
            //    if (a.Id.Equals(int.Parse(Accid.Text)) == true)
            //    {
            //        a.moenyinput(int.Parse(money.Text));
            //    }
            //}     //foreach 안먹음
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));

            if (view.Filter == null)
            {
                view.Filter = delegate (object obj)
                {
                    return ((Account)obj).Name.Equals(nametoselect.Text);

                };
            }
            else
            {
                view.Filter = null;
            }
        }

        private void BalfilterButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));

            if (view.Filter == null)
            {
                view.Filter = delegate (object obj)
                {
                    return ((Account)obj).Bal >= 50000;
                };
            }
            else
            {
                view.Filter = null;
            }
        }

        private void Sortcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acclist"));

            if (sortcombo.SelectedIndex.ToString() == "0")
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
            else if (sortcombo.SelectedIndex.ToString() == "1")
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Bal", ListSortDirection.Ascending));
            }
            else if (sortcombo.SelectedIndex.ToString() == "2")
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            }
            else
                return;


        }
    }
}
