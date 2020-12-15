using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class LibrarianRepository : RepositoryBase<Librarian>
    {
        public LibrarianRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
        public override void Create(Librarian entity)
        {
            base.Create(entity);
        }

        public override IQueryable<Librarian> FindByCondition(Expression<Func<Librarian, bool>> expression)
        {
            return base.FindByCondition(expression);
        }

        public override IQueryable<Librarian> FindAll()
        {
            return base.FindAll();
        }

        public override void Delete(Librarian entity)
        {
            base.Delete(entity);
        }

        public override void Update(Librarian entity)
        {
            base.Update(entity);
        }
    }
}
