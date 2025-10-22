using AppCore.Entities.ContractsInformation.ContractLog;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    public class ContractLogRepository : IContractLogRepository
    {
        private readonly AppDbContext _context;
        public ContractLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ContractLog log)
        {
            await _context.Set<ContractLog>().AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContractLog>> GetByContractId(Guid contractId)
        {
            return await _context.Set<ContractLog>()
                                 .AsNoTracking()
                                 .Where(x => x.ContractId == contractId)
                                 .OrderByDescending(x => x.UpdatedAt)
                                 .ToListAsync();
        }
    }
}
