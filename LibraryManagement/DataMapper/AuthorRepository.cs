using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class AuthorRepository
    {
        private readonly LibraryDbContext libraryContext;

        public AuthorRepository(LibraryDbContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        public bool AddAuthor(Author author)
        {
            libraryContext.Authors.Add(author);
            return libraryContext.SaveChanges() != 0;
        }
    }
}
