using AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    public class InvoiceBaseCustomizationRepository : IInoviceBaseCustomization
    {
        private readonly AppDbContext _context;
        public InvoiceBaseCustomizationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<InvoiceBaseCustomization> Get(Guid id)
        {
            var inv = await _context.InvoiceBaseCustomizations.FindAsync(id);
            return inv;
            
        }

        public async Task Update(InvoiceBaseCustomization entity)
        {
            var existing = await _context.InvoiceBaseCustomizations.FindAsync(entity.Id);
            if (existing != null)
            {
                throw new InvalidOperationException("");
            }
            var newInvoice = new InvoiceBaseCustomization();

            newInvoice.Title = entity.Title;
            newInvoice.Width = entity.Width;
            newInvoice.IsHidden = entity.IsHidden;

            _context.InvoiceBaseCustomizations.Add(newInvoice);
            await _context.SaveChangesAsync();

        }


    }
}
