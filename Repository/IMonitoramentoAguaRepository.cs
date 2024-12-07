using WebServiceFiap.Model;

namespace WebServiceFiap.Repository
{
    public interface IMonitoramentoAguaRepository
    {
        Task<IEnumerable<MonitoramentoAgua>> GetAllAsync(int pageNumber, int pageSize);
        Task<MonitoramentoAgua> GetByIdAsync(int id);
        Task AddAsync(MonitoramentoAgua entity);
        Task UpdateAsync(MonitoramentoAgua entity);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
    }

}
