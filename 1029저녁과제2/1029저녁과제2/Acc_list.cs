using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1029저녁과제2
{
    public class Acc_list : ObservableCollection<Acc_Io>//List<Account>
    {
        public Acc_list()
        {
            Add(new Acc_Io() { Id = "000-0000-0000-00", History= "0", Balance = 100000 });
            Add(new Acc_Io() { Id = "111-1111-1111-11", History = "0", Balance = 123450 });
            Add(new Acc_Io() { Id = "333-3333-3333-33", History = "0", Balance = 123450 });

        }
    }
}
