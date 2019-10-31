using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _1029_WPF_실습_계좌관리
{
    public class Account : INotifyPropertyChanged
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

        private int? bal;
        public int? Bal
        {
            get { return bal; }
            set
            {
                bal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Bal"));
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

        public void moenyinput(int money)
        {
            if(money <= 0)
            {
                MessageBox.Show("0원이하는 입금할 수 없습니다.");
            }

            bal += money;
        }

        public override string ToString()
        {
            return string.Format("[계좌번호] : {0}, [이름] : {1}님, [잔액] : {2}원, [개설일] : {3}", id, name, bal, date);
        }
    }
}
