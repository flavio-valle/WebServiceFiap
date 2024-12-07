using WebServiceFiap.Model;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class MonitoramentoAguaService
    {
        private readonly IMonitoramentoAguaRepository _repository;

        public MonitoramentoAguaService(IMonitoramentoAguaRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<MonitoramentoAgua> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var items = await _repository.GetAllAsync(pageNumber, pageSize);
            var total = await _repository.GetTotalCountAsync();
            return (items, total);
        }

        public async Task<MonitoramentoAgua> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(MonitoramentoAgua monitoramento)
        {
            await _repository.AddAsync(monitoramento);
        }

        public async Task UpdateAsync(int id, MonitoramentoAgua updateData)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Registro não encontrado.");

            // Atualizar propriedades:
            existing.DT_HORA = updateData.DT_HORA;
            existing.LC_LOCALIZACAO = updateData.LC_LOCALIZACAO;
            existing.QT_PH = updateData.QT_PH;
            existing.QT_OXIGENIO_DISSOLVIDO = updateData.QT_OXIGENIO_DISSOLVIDO;
            existing.QT_TURBIDEZ = updateData.QT_TURBIDEZ;
            existing.QT_COLIFORMES_TOTAIS = updateData.QT_COLIFORMES_TOTAIS;
            existing.QT_FOSFORO_TOTAL = updateData.QT_FOSFORO_TOTAL;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
