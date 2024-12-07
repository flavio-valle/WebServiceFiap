using WebServiceFiap.Model;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class MonitoramentoArService
    {
        private readonly IMonitoramentoArRepository _repository;

        public MonitoramentoArService(IMonitoramentoArRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<MonitoramentoAr> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var items = await _repository.GetAllAsync(pageNumber, pageSize);
            var total = await _repository.GetTotalCountAsync();
            return (items, total);
        }

        public async Task<MonitoramentoAr> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(MonitoramentoAr monitoramento)
        {
            await _repository.AddAsync(monitoramento);
        }

        public async Task UpdateAsync(int id, MonitoramentoAr updateData)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Registro não encontrado.");

            existing.DT_HORA = updateData.DT_HORA;
            existing.LC_LOCALIZACAO = updateData.LC_LOCALIZACAO;
            existing.QT_MONOXIDO_CARBONO = updateData.QT_MONOXIDO_CARBONO;
            existing.QT_OZONIO = updateData.QT_OZONIO;
            existing.QT_DIOXIDO_NITROGENIO = updateData.QT_DIOXIDO_NITROGENIO;
            existing.QT_DIOXIDO_ENXOFRE = updateData.QT_DIOXIDO_ENXOFRE;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
