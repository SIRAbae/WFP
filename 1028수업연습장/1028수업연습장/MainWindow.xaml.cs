using System;
using System.Collections.Generic;
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

namespace _1028수업연습장
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private People people = new People();
        private Person per;
        // { Name = "홍길동", Phone = " 010-1111-1234", Age = "25" };
        public MainWindow()
        {
            InitializeComponent();
            per = people[0];
            UpdateNameToUI();
            UpdatePhoneToUI();
            UpdateAgeToUI();

            UpdateListBox();

        }
        private void UpdateNameToUI()
        {
            if (per == null)
            {
                name.Text = "";
                nameLabel.Content = "";
            }
            else
            {
                name.Text = per.Name;
                nameLabel.Content = per.Name;
            }

        }
        private void UpdatePhoneToUI()
        {
            if (per == null)
            {
                phone.Text = "";
                phoneLabel.Content = "";
            }
            else
            {
                phone.Text = per.Phone;
                phoneLabel.Content = per.Phone;
            }
        }
        private void UpdateAgeToUI()
        {
            if (per == null)
            {
                age.Text = "";
                ageLabel.Content = "";
            }
            else
            {
                age.Text = per.Age;
                ageLabel.Content = per.Age;
            }
        }

        private void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach (Person per in people)
                listbox.Items.Add(per.ToString());
        }
        #region 데이터다루기 예제5-2
        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            // per.Name = name.Text;
            nameLabel.Content = name.Text;
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            //per.Phone = phone.Text;
            phoneLabel.Content = phone.Text;
            Title = per.Phone;
        }

        private void age_TextChanged(object sender, TextChangedEventArgs e)
        {
            //per.Age = age.Text;
            ageLabel.Content = age.Text;
        }
        #endregion
        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                per = people[listbox.SelectedIndex];
                UpdateNameToUI();
                UpdatePhoneToUI();
                UpdateAgeToUI();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedIndex >= 0)
            {
                people.RemoveAt(listbox.SelectedIndex);

                //컬렉션에 원소가 없다면 리스트의 현재 아이템이 없도록(per=null) 한다.
                if (people.Count == 0)
                    per = null;
                else
                    per = people[0];

                //모든 UI 컨트롤을 갱신한다.
                UpdateNameToUI();
                UpdatePhoneToUI();
                UpdateAgeToUI();

                UpdateListBox();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;
            people.Add(new Person() { Name = name.Text, Phone = phone.Text, Age = age.Text });
            //리스트 박스의 아이템을 갱신한다.
            UpdateListBox();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "")
                return;
            per.Name = name.Text;
            per.Phone = phone.Text;
            //모든 UI 컨트롤을 갱신한다.
            UpdateNameToUI();
            UpdatePhoneToUI();
            UpdateAgeToUI();

            UpdateListBox();

        }
    }
    //public class Person
    //{
    //    public string Name { get; set; }
    //    public string Phone { get; set; }
    //    public string Age { get; set; }

    //    public override string ToString()
    //    {
    //        return Name + " : " + Phone + " 나이 :" + Age;
    //    }
    //}

    public class People : List<Person>
    {
        public People()
        {
            Add(new Person() { ShortNumber = 1, Name = "홍길동", Phone = " 010-2778-2056", Age = "25", Male = true });
            Add(new Person() { ShortNumber = 2, Name = "일지매", Phone = " 010-0000-2056", Age = "125", Male = true });
            Add(new Person() { ShortNumber = 3, Name = "임꺽정", Phone = " 010-8888-2056", Age = "235" ,Male = true });
        }
    }
}
