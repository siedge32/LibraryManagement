using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class BookStock
    {
        public int Id { get; set; }
        public int NumberOfBooks { get; set; }
        public int NumberOfBooksForLecture { get; set; }
        public virtual BookPublication BookPublication { get; set; }
    }
}
