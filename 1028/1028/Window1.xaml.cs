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
using System.Windows.Shapes;

namespace _1028
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        Person per = null;
        public Window1()
        {
            InitializeComponent();
            per = (Person)FindResource("person");
            Validation.AddErrorHandler(shortNumber, shortNumber_ValidationError);
        }

        #region 예외 처리
        void shortNumber_ValidationError(object sender,
            ValidationErrorEventArgs e)
        {
            MessageBox.Show((string)e.Error.ErrorContent, "유효성 검사 실패");
            shortNumber.ToolTip = (string)e.Error.ErrorContent;
        }
        #endregion

        #region Clear 버튼 이벤트 핸들러
        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            per.Name = "";
            per.Phone = "";
            per.Age = null;
            per.Male = null;
        }
        #endregion

        #region Next Prev 버튼 이벤트 핸들러
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToPrevious();
            if( view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast();
            }
        }
        #endregion

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            People people = (People)FindResource("people");

            if(list.SelectedIndex >=0)
            {
                people.RemoveAt(list.SelectedIndex);
            }

        }
        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            People people = (People)FindResource("people");

            Person newperson = new Person()
            {
                ShortNumber = per.ShortNumber,
                Name = per.Name,
                Phone = per.Phone,
                Age = per.Age,
                Male = per.Male
            };

            people.Add(newperson);
        }

        //검색
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            if (view.Filter == null)
            {
                view.Filter = delegate (object obj)
                {
                    return ((Person)obj).Name == selectName.Text;
                };
            }
            else
            {
                view.Filter = null;
            }
        }
    }
}
