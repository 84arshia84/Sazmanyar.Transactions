using AppCore.Entities.Attaches;
using AppCore.Entities.Descriptions;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PublicEntitiesRepository
{
    internal class AttachRepository : IAttachRepository
    {
        private readonly AppDbContext _context;
        public AttachRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Attach attach)
        {
            try
            {
                await _context.Attaches.AddAsync(attach);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Add(List<Attach> attach)
        {
            try
            {
                await _context.Attaches.AddRangeAsync(attach);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(Guid attach)
        {
            try
            {
                var model = await Get(attach);
                if (model != null)
                {
                    _context.Attaches.Remove(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Attach> Get(Guid id)
        {
            try
            {
                return await _context.Attaches.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Attach>> GetAll(Guid sectionId)
        {
            try
            {
                return await _context.Attaches.Where(x => x.SectionId == sectionId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(Attach attach)
        {
            try
            {
                var model = await Get(attach.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(attach);
                    return;
                }
                await Add(attach);
                return;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
