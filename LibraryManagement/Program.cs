using LibraryManagement.DataMapper;
using LibraryManagement.DomainModel;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu"
            };

            var repo = new AuthorRepository(new LibraryDbContext());

            var logger = new Logger();
            logger.LogError("Hello logging world!", MethodBase.GetCurrentMethod());

            // repo.AddAuthor(author);
        }
    }
}
