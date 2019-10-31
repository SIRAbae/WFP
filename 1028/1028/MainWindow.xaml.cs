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

namespace _1028
{

    //public class People : List<Person>
    public class People : ObservableCollection<Person>
    {
        public People()
        {
            Add(new Person() { ShortNumber = 0, Male=true, Name = "홍길동", Phone = "010-1111-1234", Age = 20 });
            Add(new Person() { ShortNumber = 1, Male = false, Name = "일지매", Phone = "010-2222-1234", Age = 15 });
            Add(new Person() { ShortNumber = 2, Male = true, Name = "임꺼정", Phone = "010-3333-1234", Age = 22 });
            Add(new Person() { ShortNumber = 3, Male = false, Name = "청춘향", Phone = "010-7777-1234", Age = 19 });
        }
    }
   
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private People people = new People();
        private Person per = null;

        public MainWindow()
        {
            InitializeComponent();

            per = people[0];

            UpdateData();
            UpdateListBox();
        }

        #region data -> UI
        private void UpdateData()
        {
            UpdateNameToUI();
            UpdatePhoneToUI();
            UpdateAgeToUI();
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
                age.Text = per.Age.ToString();
                ageLabel.Content = per.Age;
            }
        }
        private void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach(Person per in people)
            {
                listbox.Items.Add(per);
            }
        }
        #endregion

        #region UI -> data
        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
  //          per.Name = name.Text;
  //          this.Title = per.Name;
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
   //         per.Phone = phone.Text;
   //         this.Title = per.Phone;
        }        

        private void age_TextChanged(object sender, TextChangedEventArgs e)
        {
   //         per.Age = int.Parse(age.Text);
   //         this.Title = per.Age.ToString();
        }

        #endregion

        #region 리스트 박스 이벤트 핸들러
        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listbox.SelectedIndex >= 0)
            {
                per = people[listbox.SelectedIndex];
                UpdateData();
            }
        }
        #endregion

        #region 버튼 이벤트 핸들러
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            people.Add(new Person() { Name = name.Text, Phone = phone.Text, Age = int.Parse(age.Text) });

            per = people[people.Count - 1];

            UpdateData();            
            UpdateListBox();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedIndex < 0)
                return;

            people.RemoveAt(listbox.SelectedIndex);

            if (people.Count == 0)
                per = null;
            else
                per = people[0];

            UpdateData();
            UpdateListBox();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            per.Name = name.Text;
            per.Phone = phone.Text;
            per.Age = int.Parse(age.Text);

            UpdateData();
            UpdateListBox();
        }
        #endregion
    }
}
