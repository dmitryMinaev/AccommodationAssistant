using AccommodationAssistant.Domain.Common;
using AccommodationAssistant.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace AccommodationAssistant.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected DataContext DbContext { get; }
        protected DbSet<TEntity> Table { get; }
        protected ILogger Logger { get; }

        public BaseRepository(DataContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
            Table = dbContext.Set<TEntity>();
        }

        public Task<TEntity> AddAsync(TEntity entity)
            => SaveChangesAndHandleExceptionAsync<TEntity>(async () =>
            {
                var result = await Table.AddAsync(entity);

                return () =>
                {
                    Array.ForEach(DbContext.ChangeTracker.Entries().ToArray(), entry => entry.State = EntityState.Detached);
                    return result.Entity;
                };
            });

        public Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null)
            => HandleExceptionAsync(async () =>
            {
                var query = Table.AsQueryable().AsNoTracking();

                if (predicate is not null)
                {
                    query = query.Where(predicate);
                }

                var result = await query.ToListAsync();

                return (IList<TEntity>)result;
            });

        public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
            => HandleExceptionAsync(async () =>
            {
                var query = Table.AsQueryable().AsNoTracking();

                var entity = await query.FirstOrDefaultAsync(predicate);

                return entity;
            });

        protected async Task<T> HandleExceptionAsync<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();

                return result;
            }
            catch (DbUpdateException e)
            {
                Logger.LogError(e, "DbUpdateException error");
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Exception error");
                throw;
            }
        }

        protected Task<T> SaveChangesAndHandleExceptionAsync<T>(Func<Task<Func<T>>> func)
        => HandleExceptionAsync(async () =>
        {
            var result = await func();
            await DbContext.SaveChangesAsync();

            return result();
        });
    }
}
