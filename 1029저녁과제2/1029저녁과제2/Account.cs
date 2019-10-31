using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _1029저녁과제2
{
    public class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
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
        private int? balance;
        public int? Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Balance"));
            }
        }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return "아이디 : " + Id + " 이름 : " + Name + "//잔액 : " + balance + " 원//" + DateTime.Now.ToLongDateString();
        }

    }
    public class ShortNumberValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "정수를 입력하세요");
            }

            if (Min <= number && number <= Max)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                string msg = string.Format("단축 번호는 {0}에서 {1}까지 가능합니다.", Min, Max);
                return new ValidationResult(false, msg);
            }
        }
    }
}