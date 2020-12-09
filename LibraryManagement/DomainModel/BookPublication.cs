using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModel
{
    public class BookPublication // book that can be rented
    {
        public int Id { get; set; }
        public int NumberOfPages { get; set; }
        public string CoverMaterial { get; set; }
        public PublishingHouse PublishingHouse { get; set; } // Edition
        public Book Book { get; set; }
        [Required]
        public virtual BookStock BookStock { get; set; }
        public virtual ICollection<BookWithdrawal> BookWithdrawals { get; set; }
    }
}
