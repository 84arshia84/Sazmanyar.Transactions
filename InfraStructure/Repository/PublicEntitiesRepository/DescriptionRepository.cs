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
    internal class DescriptionRepository : IDescriptionRepository
    {
        private readonly AppDbContext _context;
        public DescriptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Description description)
        {
            try
            {
                await _context.Descriptions.AddAsync(description);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Add(List<Description> description)
        {
            try
            {
                await _context.Descriptions.AddRangeAsync(description);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(Guid description)
        {
            try
            {
                var model = await Get(description);
                if (model != null)
                {
                    _context.Descriptions.Remove(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Description> Get(Guid id)
        {
            try
            {
                return await _context.Descriptions.FirstOrDefaultAsync(x=>x.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Description>> GetAll(Guid sectionId)
        {
            try
            {
                return await _context.Descriptions.Where(x=>x.SectionId == sectionId).OrderBy(x=>x.CreateTime).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(Description description)
        {
            try
            {
                var model = await Get(description.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(description);
                    return;
                }
                await Add(description);
                return;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
