using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class FieldRepository : RepositoryBase<Field>
    {
        public FieldRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
        public override void Create(Field entity)
        {
            base.Create(entity);
        }

        public override IQueryable<Field> FindByCondition(Expression<Func<Field, bool>> expression)
        {
            return base.FindByCondition(expression);
        }

        public override IQueryable<Field> FindAll()
        {
            return base.FindAll();
        }

        public override void Delete(Field entity)
        {
            base.Delete(entity);
        }

        public override void Update(Field entity)
        {
            base.Update(entity);
        }
    }
}
