using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1029저녁과제
{
    public class AccountIolist : INotifyPropertyChanged
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
                    PropertyChanged(this, new PropertyChangedEventArgs("Id1"));
            }
        }


        private int? input;
        public int? Input
        {
            get { return input; }
            set
            {
                input = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Input"));
            }
        }

        private int? output;
        public int? Output
        {
            get { return output; }
            set
            {
                output = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Output"));
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
        private DateTime Date { get; set; }

        

        public override string ToString()
        {
            return "아이디 : " + Id + " 입금 : " + Input + "// 출금 : " + Output + "//잔액" + balance + " 원//" + DateTime.Now.ToLongDateString();
        }
    }
}
