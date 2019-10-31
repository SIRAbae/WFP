using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace _1028수업연습장
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int? shortNumber;
        public int? ShortNumber
        {
            get { return shortNumber; }
            set
            {
                shortNumber = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ShortNumber"));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }
        private string age;
        public string Age
        {
            get { return age; }
            set
            {
                age = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
        }
        public bool? male;
        public bool? Male
        {
            get { return male; }
            set
            {
                male = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Male"));
            }
        }
        public override string ToString()
        {
            return Name + " : " + Phone + " : " + Age + " : " + Male;
        }
    }

    [ValueConversion(/*원본형식*/typeof(bool), /*대상 형식*/typeof(bool))]
    public class MaleToFemaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                return null;

            bool? male = (bool?)value;

            if (male == null)
                return null;

            else
                return !(bool?)value;
        }

        //UI속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                return null;

            bool? male = (bool?)value;

            if (male == null)
                return null;

            else
                return !(bool?)value;
        }
    }

    // 형식변환기 : bool <-> string
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(bool?))
            //    return null;
            bool? male = (bool?)value;
            if (male == null)
                return "";
            else if (male == true)
            {
                return "남성";
            }
            else
                return "여성";
        }

        //UI속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (targetType != typeof(bool?))
            //    return null;

            string male = (string)value;

            if (male == "")
                return null;

            else if (male == "남성")
                return true;

            else
                return false;
        }
    }
    public class ShortNumberValidationRule : ValidationRule
    {
        int min;
        public int Min { get { return min; } set { min = value; } }

        int max; 
        public int Max { get { return max; } set { max = value; } }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "정수를 입력하세요.");
            }

            if (min <= number && number <= max)
            {   // new ValidationResult(true, null) 같다
                return ValidationResult.ValidResult;
            }
            else
            {
                string msg = string.Format("단축 번호는 {0}에서 {1}까지 입력 가능합니다.", min, max);
                return new ValidationResult(false, msg);
            }
        }
    }
}