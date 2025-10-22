using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Enums;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class ContractTypeRepository : IContractTypeRepository
    {
        private readonly AppDbContext _context;
        public ContractTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(ContractType contractType)
        {
            try
            {
                if (!ContractTypeCrudValidation.CheckDuplicate(_context, contractType))
                {
                    await _context.ContractTypes.AddAsync(contractType);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateOfficeOnline(Guid contractTypeId,Guid? filenameId)
        {
            try
            {
                var model = await _context.ContractTypes.FirstOrDefaultAsync(x => x.ID == contractTypeId);
                if (model != null)
                {
                    model.OfficeOnlineDocumentId = filenameId;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid contractType)
        {
            try
            {
                if (!ContractTypeCrudValidation.CheckIsItUsed(_context, contractType))
                {
                    var model = await Get(contractType);
                    _context.ContractTypes.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContractType> Get(Guid contractType)
        {
            try
            {
                return await _context.ContractTypes.FirstOrDefaultAsync(x => x.ID == contractType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContractType>> GetAll()
        {
            try
            {
                return await _context.ContractTypes.OrderBy(x => x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(ContractType contractType)
        {
            try
            {
                if (!ContractTypeCrudValidation.CheckDuplicate(_context, contractType))
                {
                    var model = await Get(contractType.ID);
                    if (model != null)
                    {
                        _context.Entry(model).CurrentValues.SetValues(contractType);
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
