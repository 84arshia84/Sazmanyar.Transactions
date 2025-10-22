using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.Histories;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository.HistoryRepository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly AppDbContext _context;
        public HistoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(History history)
        {
            try
            {
                await _context.Histories.AddAsync(history);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<History>> GetAll(Guid EntityId)
        {
            try
            {
                return await _context.Histories.Where(c => c.EntityId == EntityId).OrderByDescending(c => c.InsertDate).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
