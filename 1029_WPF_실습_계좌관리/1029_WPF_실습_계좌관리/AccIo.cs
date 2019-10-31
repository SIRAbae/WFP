using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1029_WPF_실습_계좌관리
{
    public class AccIo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        private string history;
        public string History
        {
            get { return history; }
            set
            {
                history = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("History"));
            }
        }

        private int? moeny;
        public int? Money
        {
            get { return moeny; }
            set
            {
                moeny = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Money"));
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

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }
    }
}
