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
using System.Windows.Shapes;

namespace _1028수업연습장
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        //private Person per = new Person() { Name = "홍길동", Phone = "010-1111-1234", Age ="00" };

        //public Window1()
        //{
        //    InitializeComponent();
        //    panel.DataContext = per;
        //}
        People people = new People(); 
       private Person per = null;
        public Window1()
        {
            InitializeComponent();
            //per = (Person)FindResource("person");

            Validation.AddErrorHandler(shortNumver, shortNumver_ValidationError);

            panel2.DataContext = people;
        }
        void shortNumver_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show((string)e.Error.ErrorContent, "유효성 검사 실패");
            shortNumver.ToolTip = (string)e.Error.ErrorContent;
        }
        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            per.ShortNumber = null;
            per.Name = "";
            per.Phone = "";
            per.Age = "";
            per.Male = null;

        }
    }
    //public class Person1
    //{        
    //    public string Name1 { get; set; }
    //    public string Phone1 { get; set; }
    //}
 
}
