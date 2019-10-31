using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1029_WPF_실습_계좌관리
{
    public class AccList : ObservableCollection<Account>
    {
        public AccList()
        {
            //Add(new Account() { Id = 10000, Name = "김경훈", Bal = 10000, Date = DateTime.Now });
            //Add(new Account() { Id = 20000, Name = "정찬묵", Bal = 20000, Date = DateTime.Now });
            //Add(new Account() { Id = 30000, Name = "정영진", Bal = 30000, Date = DateTime.Now });
        }

    }
}
