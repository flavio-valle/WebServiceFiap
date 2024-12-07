using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Model;
using WebServiceFiap.Repository.Context;

namespace WebServiceFiap.Repository
{
    public class MonitoramentoAguaRepository : IMonitoramentoAguaRepository
    {
        private readonly ContextBase _context;

        public MonitoramentoAguaRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonitoramentoAgua>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.TB_MONITORAMENTO_AGUA
                .OrderBy(m => m.ID_MONITORAMENTO_AGUA)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MonitoramentoAgua> GetByIdAsync(int id)
        {
            return await _context.TB_MONITORAMENTO_AGUA
                .FirstOrDefaultAsync(m => m.ID_MONITORAMENTO_AGUA == id);
        }

        public async Task AddAsync(MonitoramentoAgua entity)
        {
            await _context.TB_MONITORAMENTO_AGUA.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MonitoramentoAgua entity)
        {
            _context.TB_MONITORAMENTO_AGUA.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.TB_MONITORAMENTO_AGUA.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.TB_MONITORAMENTO_AGUA.CountAsync();
        }
    }
}
