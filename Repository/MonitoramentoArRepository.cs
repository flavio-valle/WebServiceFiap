using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Model;
using WebServiceFiap.Repository.Context;

namespace WebServiceFiap.Repository
{
    public class MonitoramentoArRepository : IMonitoramentoArRepository
    {
        private readonly ContextBase _context;

        public MonitoramentoArRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonitoramentoAr>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.TB_MONITORAMENTO_AR
                .OrderBy(m => m.ID_MONITORAMENTO_AR)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MonitoramentoAr> GetByIdAsync(int id)
        {
            return await _context.TB_MONITORAMENTO_AR
                .FirstOrDefaultAsync(m => m.ID_MONITORAMENTO_AR == id);
        }

        public async Task AddAsync(MonitoramentoAr entity)
        {
            await _context.TB_MONITORAMENTO_AR.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MonitoramentoAr entity)
        {
            _context.TB_MONITORAMENTO_AR.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.TB_MONITORAMENTO_AR.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.TB_MONITORAMENTO_AR.CountAsync();
        }
    }
}
