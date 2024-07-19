namespace AMAK.Application.Interfaces {
    public interface IRepository<TEntity>
        where TEntity : class {
        void Add(TEntity obj);
        bool IsExistById(Guid id);
        Task<TEntity?> GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        Task<int> SaveChangesAsync();

    }
}