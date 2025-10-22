using AppCore.Entities.InvoicesInformations.InvoiceType;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class InvoiceTypeRepository : IInvoiceTypeRepository
    {
        private readonly AppDbContext _context;
        public InvoiceTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<InvoiceType>> GetAll()
        {
            try
            {
                var result = await _context.InvoiceTypes.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
