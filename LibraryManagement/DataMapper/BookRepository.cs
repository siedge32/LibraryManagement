using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class BookRepository : RepositoryBase<Book>
    {
        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }

        public override void Create(Book entity)
        {
            base.Create(entity);
        }

        public override IQueryable<Book> FindByCondition(Expression<Func<Book, bool>> expression)
        {
            return base.FindByCondition(expression);
        }

        public override IQueryable<Book> FindAll()
        {
            return base.FindAll();
        }

        public override void Delete(Book entity)
        {
            base.Delete(entity);
        }

        public override void Update(Book entity)
        {
            base.Update(entity);
        }
    }
}
