using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Field> Categories { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<BookPublication> BookPublications { get; set; }
    }
}
