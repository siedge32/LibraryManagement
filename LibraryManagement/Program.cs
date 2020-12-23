using LibraryManagement.BusinessLayer;
using LibraryManagement.DataMapper;
using LibraryManagement.DomainModel;
using LibraryManagement.Utils;
using Microsoft.Practices.EnterpriseLibrary.Validation;
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
            var dbContex = new LibraryDbContext();

            // Entity Framework 6 TEST
            //var repobase = new RepositoryBase<Author>(dbContex);

            //var authorsThatMeetCondition = repobase.FindByCondition(a => a.Id == 1);
            //if(authorsThatMeetCondition.Count() == 1)
            //{
            //    var myAuthor = authorsThatMeetCondition.First();
            //    myAuthor.Adress = "Prunului 14";
            //    repobase.Update(myAuthor);

            //}

            //repobase.Create(author);


            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Art - Science",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author> { author }
            };

            var pH = new PublishingHouse
            {
                Name = "Corint"
            };

            var bookStock = new BookStock
            {
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 7
            };

            var bookpH = new BookPublication
            {
                NumberOfPages = 36,
                CoverMaterial = "Musama",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            
            var wasCreated = new BookPublicationService(new BookPublicationRepository(dbContex)).CreateBookPublication(bookpH);



            //var wasCreated = new BookService(new BookRepository(dbContex)).CreateBook(book);


            // dbContex.Database.Delete();

        }
    }
}
