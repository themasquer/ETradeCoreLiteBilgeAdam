using AppCore.Records.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.Bases
{
    /// <summary>
    /// Service class for managing data access operations without using models.
    /// </summary>
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        private readonly DbContext _dbContext;
        private string _resultMessage = "Changes not saved.";

        protected ServiceBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public virtual Result Add(TEntity entity, bool save = true)
        {
            _dbContext.Add(entity);
            if (save)
            {
                Save();
                return new SuccessResult();
            }
            return new ErrorResult(_resultMessage);
        }

        public virtual Result Update(TEntity entity, bool save = true)
        {
            _dbContext.Update(entity);
            if (save)
            {
                Save();
                return new SuccessResult();
            }
            return new ErrorResult(_resultMessage);
        }

        public virtual Result Delete(TEntity entity, bool save = true)
        {
            _dbContext.Remove(entity);
            if (save)
            {
                Save();
                return new SuccessResult();
            }
            return new ErrorResult(_resultMessage);
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
                return new SuccessResult();
            }
            return new ErrorResult(_resultMessage);
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
        }
    }
}
