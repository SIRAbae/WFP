using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace _1028저녁과제
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private People people = new People();
        private Account acc;

        public MainWindow()
        {
            InitializeComponent();
            acc = people[0];
            cmbColors.ItemsSource = typeof(Colors).GetProperties();
            ComboColors.ItemsSource = typeof(Colors).GetProperties();
            UpdateListBox();
           
        }

        private void cmbColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Color selectedColor1 = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
            this.Background = new SolidColorBrush(selectedColor1);
            Backtext.Background = new SolidColorBrush(selectedColor1);
        }

        private void ComboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Color selectedColor2 = (Color)(ComboColors.SelectedItem as PropertyInfo).GetValue(null, null);
            Dontext.Text = "출력되는글자색입니다.";
            Dontext.Foreground = new SolidColorBrush(selectedColor2);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Id.Text == "" || Balance.Text == "")
                return;
            people.Add(new Account() { Name = Name.Text, Id = Id.Text, Balance = Balance.Text });
            //리스트 박스의 아이템을 갱신한다.
            UpdateListBox();
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                people.RemoveAt(listbox.SelectedIndex);

                //컬렉션에 원소가 없다면 리스트의 현재 아이템이 없도록(per=null) 한다.
                if (people.Count == 0)
                    acc = null;
                else
                    acc = people[0];

                //모든 UI 컨트롤을 갱신한다.
                UpdateNameToUI();
                UpdateIdToUI();
                UpdateBalanceToUI();

                UpdateListBox();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Id.Text == "")
                return;
            acc.Name = Name.Text;
            acc.Balance = Balance.Text;
            //모든 UI 컨트롤을 갱신한다.
            UpdateNameToUI();
            UpdateIdToUI();
            UpdateBalanceToUI();

            UpdateListBox();
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                acc = people[listbox.SelectedIndex];
                UpdateNameToUI();
                UpdateIdToUI();
                UpdateBalanceToUI();
            }
        }
        private void UpdateNameToUI()
        {
            if (acc == null)
            {
                Name.Text = "";
                //nameLabel.Content = "";
            }
            else
            {
                Name.Text = acc.Name;
                //nameLabel.Content = acc.Name;
            }

        }
        private void UpdateIdToUI()
        {
            if (acc == null)
            {
               Id.Text = "";
                //phoneLabel.Content = "";
            }
            else
            {
                Id.Text = acc.Id;
               // phoneLabel.Content = acc.Phone;
            }
        }
        private void UpdateBalanceToUI()
        {
            if (acc == null)
            {
                Balance.Text = "";
                //ageLabel.Content = "";
            }
            else
            {
                Balance.Text = acc.Balance;
               //ageLabel.Content = acc.Age;
            }
        }

        private void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach (Account acc in people)
                listbox.Items.Add(acc.ToString());
        }
    }
    public class People : List<Account>
    {
        public People()
        {
            Add(new Account() { Name = "홍길동", Id = " 352-0000-0000-00", Balance = "100000" });
           
        }
    }
}


