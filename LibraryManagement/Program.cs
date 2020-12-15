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

        static void LogResults(ValidationResults results)
        {
            foreach (var result in results)
            {
                //TO DO : basically... log!
                Console.WriteLine("Validation error: " + result.Message);
            }
        }

        static ValidationResults ValidateEntity<T>(string ruleSet, T entity)
        {
            var validator = ValidationFactory.CreateValidator<T>(ruleSet);
            var results = new ValidationResults();
            validator.Validate(entity, results);

            return results;
        }

        static void Main(string[] args)
        {

            var dbContex = new LibraryDbContext();

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganusad",
                Gender = "F"

            };

            // var ruleSets = new List<string> { "AuthorFieldNotNull", "AuthorNameStringLength", "AuthorNameRegex", "AuthorInvalidGender" };

            //foreach (var ruleSet in ruleSets)
            //{
            //    // author = null;
            //    var results = ValidateEntity(ruleSet, author);
            //    if (!results.IsValid)
            //    {
            //        LogResults(results);
            //        break;
            //    }
            //}


            // Entity Framework 6 TEST
            var repobase = new RepositoryBase<Author>(dbContex);

            //var authorsThatMeetCondition = repobase.FindByCondition(a => a.Id == 1);
            //if(authorsThatMeetCondition.Count() == 1)
            //{
            //    var myAuthor = authorsThatMeetCondition.First();
            //    myAuthor.Adress = "Prunului 14";
            //    repobase.Update(myAuthor);

            //}

            //repobase.Create(author);
        }
    }
}
