using System.Linq.Expressions;

namespace AccommodationAssistant.Domain.Common
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        public Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null);
        public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity> AddAsync(TEntity model);
    }
}
