using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class ReaderRepository : RepositoryBase<Reader>
    {
        public ReaderRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }

        public override void Create(Reader entity)
        {
            base.Create(entity);
        }

        public override IQueryable<Reader> FindByCondition(Expression<Func<Reader, bool>> expression)
        {
            return base.FindByCondition(expression);
        }

        public override IQueryable<Reader> FindAll()
        {
            return base.FindAll();
        }

        public override void Delete(Reader entity)
        {
            base.Delete(entity);
        }

        public override void Update(Reader entity)
        {
            base.Update(entity);
        }
    }
}
