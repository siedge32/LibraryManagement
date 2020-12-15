using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected LibraryDbContext LibraryDbContext { get; set; }

        private ILogger logger { get; set; }

        public RepositoryBase(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
            logger = new Logger();
        }

        public virtual void Create(T entity)
        {
            logger.LogInfo($"Creating a new {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Set<T>().Add(entity);
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Error on creating {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }

        public virtual void Delete(T entity)
        {
            logger.LogInfo($"Deleting {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Entry(entity).State = EntityState.Deleted;
                this.LibraryDbContext.Set<T>().Remove(entity);
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Error on Deleting {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }

        public virtual IQueryable<T> FindAll()
        {
            logger.LogInfo($"Find all", MethodBase.GetCurrentMethod());
            try
            {
                return this.LibraryDbContext.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Find all, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            logger.LogInfo($"FindByCondition", MethodBase.GetCurrentMethod());
            try
            {
                return LibraryDbContext.Set<T>().Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"FindByCondition, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }

        public virtual void Update(T entity)
        {
            logger.LogInfo($"Update {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Entry(entity).State = EntityState.Modified;
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Update {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }
    }
}
