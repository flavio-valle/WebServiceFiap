using WebServiceFiap.Model;

namespace WebServiceFiap.Repository
{
    public interface IMonitoramentoArRepository
    {
        Task<IEnumerable<MonitoramentoAr>> GetAllAsync(int pageNumber, int pageSize);
        Task<MonitoramentoAr> GetByIdAsync(int id);
        Task AddAsync(MonitoramentoAr entity);
        Task UpdateAsync(MonitoramentoAr entity);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
