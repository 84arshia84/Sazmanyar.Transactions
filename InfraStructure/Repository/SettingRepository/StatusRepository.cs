using AppCore.Entities.SettingEntities.Statuses;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;
        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Status status)
        {
            try
            {
                if (!StatusCrudvalidation.CheckDuplicate(_context, status))
                {
                    await _context.Statuses.AddAsync(status);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Guid statusId)
        {
            try
            {
                if (!ActivitycenterCrudValidation.CheckIsItUsed(_context, statusId))
                {
                    var existingActivityCenter = await Get(statusId);
                    if (existingActivityCenter != null)
                    {
                        existingActivityCenter.IsDeleted = true;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Status> Get(Guid statusId)
        {
            try
            {
                return await _context.Statuses.FirstOrDefaultAsync(x => x.ID == statusId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Status>> GetAll()
        {
            try
            {
                return await _context.Statuses.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Status> GetByContractId(Guid contractId)
        {
            try
            {
                var contract = await _context.Contracts.Include(x=>x.Status).FirstOrDefaultAsync(x=>x.ID == contractId);
                return contract.Status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Status status)
        {
            try
            {
                var model = await _context.Statuses.FirstOrDefaultAsync(y => y.ID == status.ID);
                if (model != null)
                {
                    model.Title = status.Title;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
