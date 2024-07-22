namespace AMAK.Application.Interfaces {
    public interface IRepository<TEntity> where TEntity : class {
        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> objs);
        bool IsExistById(Guid id);
        Task<TEntity?> GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void UpdateRange(IEnumerable<TEntity> objs);
        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> objs);
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}