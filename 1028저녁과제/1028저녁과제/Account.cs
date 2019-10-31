using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1028저녁과제
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
        private string balance;
        public string Balance
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
            return Id + " : " + Name + " : " + balance +" 원" + DateTime.Now.ToLongDateString();
        }

    }
}
