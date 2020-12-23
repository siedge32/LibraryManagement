﻿// <copyright file="RepositoryBase.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using LibraryManagement.Utils;

    /// <summary>
    /// The RepositoryBase class
    /// </summary>
    /// <typeparam name="T">The data model entity</typeparam>
    /// <seealso cref="LibraryManagement.DataMapper.IRepositoryBase{T}" />
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public RepositoryBase(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the library database context.
        /// </summary>
        /// <value>
        /// The library database context.
        /// </value>
        protected LibraryDbContext LibraryDbContext { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger Logger { get; set; }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Create(T entity)
        {
            this.Logger.LogInfo($"Creating a new {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Set<T>().Add(entity);
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Error on creating {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            this.Logger.LogInfo($"Deleting {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Entry(entity).State = EntityState.Deleted;
                LibraryDbContext.Set<T>().Remove(entity);
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Error on Deleting {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>
        /// Returns all entities
        /// </returns>
        public virtual IQueryable<T> FindAll()
        {
            this.Logger.LogInfo($"Find all", MethodBase.GetCurrentMethod());
            try
            {
                return LibraryDbContext.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Find all, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Return entities that met the condition
        /// </returns>
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            this.Logger.LogInfo($"FindByCondition", MethodBase.GetCurrentMethod());
            try
            {
                return LibraryDbContext.Set<T>().Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"FindByCondition, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(T entity)
        {
            this.Logger.LogInfo($"Update {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                LibraryDbContext.Entry(entity).State = EntityState.Modified;
                LibraryDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Update {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }
        }
    }
}
