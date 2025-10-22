using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class CheckListRepository : ICheckListRepository
    {
        private readonly AppDbContext _context;
        public CheckListRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(CheckList checkList)
        {
            try
            {
                if (!CheckListCrudValidation.CheckDuplicate(_context, checkList))
                {
                    await _context.CheckLists.AddAsync(checkList);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> Delete(Guid checkList,Guid contractTypeId)
        {
            try
            {
                if (!CheckListCrudValidation.CheckIsItUsed(_context, contractTypeId))
                {
                    var model = await Get(checkList);
                    _context.CheckLists.Remove(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CheckList> Get(Guid checkList)
        {
            try
            {
                return await _context.CheckLists.Where(x => x.Id == checkList).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CheckList>> GetAll(Guid contractTypeId)
        {
            try
            {
                return await _context.CheckLists.Where(x=>x.ContractTypeId==contractTypeId).OrderBy(x=>x.Order).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Update(CheckList checkList)
        {
            try
            {
                if (!CheckListCrudValidation.CheckDuplicate(_context, checkList))
                {
                    var model = await Get(checkList.Id);
                    if (model != null)
                    {
                        _context.Entry(model).CurrentValues.SetValues(checkList);
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
