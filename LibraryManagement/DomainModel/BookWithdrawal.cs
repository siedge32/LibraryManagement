using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class BookWithdrawal
    {
        public int Id { get; set; }
        public DateTime DateToReturn { get; set; }
        public Reader Reader { get; set; }
        public Librarian Librarian { get; set; }
        public virtual ICollection<BookPublication> BookPublications { get; set; }
        public virtual ICollection<Extension> Extensions { get; set; }
    }
}
