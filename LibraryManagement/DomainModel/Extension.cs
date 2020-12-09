using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class Extension
    {
        public int Id { get; set; }
        public DateTime DateToReturn { get; set; }
        public BookWithdrawal BookWithdrawal { get; set; }
    }
}
