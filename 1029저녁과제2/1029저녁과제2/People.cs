using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1029저녁과제2
{
    public class People : ObservableCollection<Account>//List<Account>
    {
        public People()
        {
            Add(new Account() { Name = "홍길동", Id = "000-0000-0000-00", Balance = 100000 });
            Add(new Account() { Name = "김길동", Id = "111-1111-1111-11", Balance = 123450 });
            Add(new Account() { Name = "고길동", Id = "333-3333-3333-33", Balance = 123450 });

        }
    }
}
