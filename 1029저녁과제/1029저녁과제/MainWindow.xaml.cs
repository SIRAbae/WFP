using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _1029저녁과제
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private People people = new People();
        Account acc = null;
        AccountIolist acclist = null;
        public MainWindow()
        {
            InitializeComponent();
            acc = (Account)FindResource("account");
            acclist = (AccountIolist)FindResource("accountiolist");
            // Validation.AddErrorHandler(shortNumber, shortNumber_ValidationError);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            People people = (People)FindResource("people");

            Account newaccount = new Account()
            {
                Id = acc.Id,
                Name = acc.Name,
                Balance = acc.Balance
            };

            people.Add(newaccount);
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
            Accountlist accountlist = (Accountlist)FindResource("accountlist");

            AccountIolist newaccountio = new AccountIolist()
            {
                Id = acclist.Id,
                Balance = acclist.Balance,
               Input = acclist.Input
            };
            InputMoney();
            accountlist.Add(newaccountio);

        }
        private void InputMoney()
        {
            if (acclist.Id == acc.Id)
            {
                if (int.Parse(Input.Text) != 0)
                {
                    if (int.Parse(Input.Text) > 0)
                    {
                        AccountIolist newaccountio = new AccountIolist()
                        {
                            Balance = acclist.Input + acclist.Balance
                        };

                    }
                    else
                        MessageBox.Show("0보다 큰 금액을 주세요");
                }
                else
                    MessageBox.Show("0이 아닌 금액을 주세요");
            }
            else
                MessageBox.Show("일치하는 아이디가 엄서요");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class People : ObservableCollection<Account>//List<Account>
    {
        public People()
        {
            Add(new Account() { Name = "홍길동", Id = "000-0000-0000-00", Balance = 100000});
            Add(new Account() { Name = "김길동", Id = "111-1111-1111-11", Balance = 123450 });
            Add(new Account() { Name = "고길동", Id = "333-3333-3333-33", Balance = 123450 });

        }
    }
    public class Accountlist : ObservableCollection<AccountIolist>//List<Account>
    {
        public Accountlist()
        {
            Add(new AccountIolist() { Id = "000-0000-0000-00", Input = 0, Output = 0, Balance = 100000 });
            Add(new AccountIolist() { Id = "111-1111-1111-11", Input = 0, Output = 0, Balance = 123450 });
            Add(new AccountIolist() { Id = "333-3333-3333-33", Input = 0, Output = 0, Balance = 123450 });

        }
    }
}
