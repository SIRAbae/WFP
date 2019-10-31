using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _1029저녁과제2
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        
        People people = null;
        Acc_list acclist = null;
        public MainWindow()
        {
            InitializeComponent();
            people = (People)FindResource("people");
            acclist = (Acc_list)FindResource("acc_list");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (accName.Text == "" || Id.Text == "")
            {
                MessageBox.Show("계좌ID와 이름은 필수 입력입니다.");
                return;
            }
            Account acc = new Account()
            {
                Id = Id.Text,
                Name = accName.Text,
                Balance = int.Parse(balance1.Text),
                Date = DateTime.Now
            };

            people.Add(acc);

            MessageBox.Show("계좌가 생성되었습니다.");


            //People people = (People)FindResource("people");

            //Account newaccount = new Account()
            //{
            //    Id = acc.Id,
            //    Name = acc.Name,
            //    Balance = acc.Balance
            //};

            //people.Add(newaccount);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            People people = (People)FindResource("people");
            if (listbox1.SelectedIndex >= 0)
            {
                people.RemoveAt(listbox1.SelectedIndex);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acc_list"));
            Account acc = (Account)view.CurrentItem;        //리스트에서 찾은거를 객체화해줌.

            if (int.Parse(money.Text) <= 0)
            {
                MessageBox.Show("0원이하 입금불가");
                return;
            }
            acc.Balance += int.Parse(money.Text);
        }

        private void sortcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("acc_list"));
            Account acc = (Account)view.CurrentItem;        //리스트에서 찾은거를 객체화해줌.

            if (int.Parse(money.Text) <= 0)
            {
                MessageBox.Show("0원이하 출금불가");
                return;
            }
            else if (int.Parse(money.Text) > acc.Balance)
            {
                MessageBox.Show("잔액보다 높은금액 출금불가");
                return;
            }
            acc.Balance -= int.Parse(money.Text);



            Acc_Io accio = new Acc_Io()
            {
                Id = labelId1.Text,
                History = Button1.Content.ToString(),
                Money = int.Parse(money.Text),
                Balance = int.Parse(acciobal.Text),
                Date = DateTime.Now
            };

            acclist.Add(accio);
            //listbox2.Items.Add(accio);
        }
    }
    
    
}
