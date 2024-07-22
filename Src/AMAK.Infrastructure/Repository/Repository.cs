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

        public virtual void AddRange(IEnumerable<TEntity> objs) {
            _dbSet.AddRange(objs);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> objs) {
            _dbSet.UpdateRange(objs);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> objs) {
            _dbSet.RemoveRange(objs);
        }

        public bool IsExistById(Guid id) {
            return _dbSet.Find(id) != null;
        }

        public async Task<int> SaveChangesAsync() {
            return await _db.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync() {
            await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync() {
            if (_db.Database.CurrentTransaction != null) {
                await _db.Database.CurrentTransaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync() {
            if (_db.Database.CurrentTransaction != null) {
                await _db.Database.CurrentTransaction.RollbackAsync();
            }
        }
    }
}