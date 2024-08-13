namespace AMAK.Application.Providers.Cache {
    public interface ICacheService {
        Task<T?> GetData<T>(string key);

        Task<object> SetData<T>(string key, T value, DateTimeOffset time);

        Task<bool> RemoveData(string key);
    }
}