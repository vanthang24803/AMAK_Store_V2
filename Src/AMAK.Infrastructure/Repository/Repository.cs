using AMAK.Application.Interfaces;
using AMAK.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Infrastructure.Repository {
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(ApplicationDbContext context) {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj) {
            _dbSet.Add(obj);
        }

        public virtual async Task<TEntity?> GetById(Guid id) {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll() {
            return _dbSet;
        }

        public virtual void Update(TEntity obj) {
            _dbSet.Update(obj);
        }

        public virtual void Remove(TEntity entity) {
            _dbSet.Remove(entity);
        }

        public bool IsExistById(Guid id) {
            var entity = _dbSet.Find(id);
            if (entity == null) {
                return false;
            }
            return true;
        }

        public async Task<int> SaveChangesAsync() {
            return await _db.SaveChangesAsync();
        }
    }
}