using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class Author
    {
        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The FirstName cannot be null", Ruleset = "AuthorFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The FirstName should have between {3} and {5} letters", Ruleset = "AuthorNameStringLength")]
        [RegexValidator(@"^[a-zA-Z]+$", MessageTemplate = "Only characters", Ruleset = "AuthorNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "AuthorNameRegex")]
        public string FirstName { get; set; }

        [NotNullValidator(MessageTemplate = "The SecondName cannot be null", Ruleset = "AuthorFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The LastName should have between {3} and {5} letters", Ruleset = "AuthorNameStringLength")]
        [RegexValidator(@"^[a-zA-Z]+$", MessageTemplate = "Only characters", Ruleset = "AuthorNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "AuthorNameRegex")]
        public string LastName { get; set; }
        public string Adress { get; set; }

        [NotNullValidator(MessageTemplate = "The Gender cannot be null", Ruleset = "AuthorFieldNotNull")]
        [DomainValidator("m", "f", "M", "F", MessageTemplate = "Unknown gender", Ruleset ="AuthorInvalidGender")]
        public string Gender { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
