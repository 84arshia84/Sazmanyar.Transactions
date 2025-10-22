using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class RoleOfOrganizationRepository : IRoleOfOrganizationRepository
    {
        private readonly AppDbContext _context;
        public RoleOfOrganizationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(RoleOfOrganization roleOfOrganization)
        {
            try
            {
                if (!RoleOfOrganizationCrudValidation.CheckDuplicate(_context, roleOfOrganization))
                {
                    await _context.RoleOfOrganizations.AddAsync(roleOfOrganization);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Guid roleOfOrganization)
        {
            try
            {
                if (!RoleOfOrganizationCrudValidation.CheckIsItUsed(_context, roleOfOrganization))
                {
                    var model = await Get(roleOfOrganization);
                    _context.RoleOfOrganizations.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RoleOfOrganization> Get(Guid roleOfOrganization)
        {
            try
            {
                return await _context.RoleOfOrganizations.FirstOrDefaultAsync(x => x.ID == roleOfOrganization);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RoleOfOrganization>> GetAll()
        {
            try
            {
                return await _context.RoleOfOrganizations.OrderBy(x => x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(RoleOfOrganization roleOfOrganization)
        {
            try
            {
                if (!RoleOfOrganizationCrudValidation.CheckDuplicate(_context, roleOfOrganization))
                {
                    var model = await Get(roleOfOrganization.ID);
                    if (model != null)
                    {
                        _context.Entry(model).CurrentValues.SetValues(roleOfOrganization);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
