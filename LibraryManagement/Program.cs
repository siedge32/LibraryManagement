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
            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganusad",
                Gender = "F"

            };

            var ruleSets = new List<string> { "AuthorNotNull", "AuthorNameStringLength", "AuthorNameRegex", "AuthorInvalidGender" };

            foreach (var ruleSet in ruleSets)
            {
                var results = ValidateEntity(ruleSet, author);
                if (!results.IsValid)
                {
                    LogResults(results);
                    break;
                }
            }


            // Entity Framework 6 TEST
            // var repo = new AuthorRepository(new LibraryDbContext());
            // var logger = new Logger();
            // logger.LogError("Hello logging world!", MethodBase.GetCurrentMethod());
            // repo.AddAuthor(author);
        }
    }
}
