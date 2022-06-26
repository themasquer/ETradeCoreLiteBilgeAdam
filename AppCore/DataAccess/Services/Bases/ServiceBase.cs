using AppCore.Records.Bases;
using AppCore.Results;
using AppCore.DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.Services.Bases
{
    /// <summary>
    /// Service class for managing data access operations without using models.
    /// </summary>
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        const string _errorMessage = "Changes not saved!";
        
        protected readonly DbContext _dbContext;

        protected ServiceBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual List<TEntity> GetList()
        {
            return Query().ToList();
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).ToList();
        }

        public virtual TEntity GetItem(int id)
        {
            return Query().SingleOrDefault(q => q.Id == id);
        }

        public virtual Result Add(TEntity entity, bool save = true)
        {
            _dbContext.Add(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Added successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Update(TEntity entity, bool save = true)
        {
            _dbContext.Update(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Updated successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Delete(TEntity entity, bool save = true)
        {
            _dbContext.Remove(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Deleted successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = Query().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                Delete(entity, false);
            }
            if (save)
            {
                Save();
                return new SuccessResult("Deleted successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
