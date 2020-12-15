﻿using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class AuthorRepository : RepositoryBase<Author>
    {
        public AuthorRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }

        public override void Create(Author entity)
        {
            base.Create(entity);
        }

        public override IQueryable<Author> FindByCondition(Expression<Func<Author, bool>> expression)
        {
            return base.FindByCondition(expression);
        }

        public override IQueryable<Author> FindAll()
        {
            return base.FindAll();
        }

        public override void Delete(Author entity)
        {
            base.Delete(entity);
        }

        public override void Update(Author entity)
        {
            base.Update(entity);
        }
    }
}
