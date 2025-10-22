using AppCore.Entities.SettingEntities.StagesRoles;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class StagesRolesRepository : IStagesRolesRepository
    {
        private readonly AppDbContext _context;
        public StagesRolesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccss)> Add(StagesRoles stagesRoles)
        {
            try
            {
                var result = _context.StagesRoles.AddAsync(stagesRoles);
                await _context.SaveChangesAsync();
                if (result.IsCompleted)
                {
                    return ("ثبت با موفقیت انجام شد.", true);
                }
                return ("ثبت با خطا روبرو شد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccss)> Delete(Guid stagesRolesId)
        {
            try
            {
                var existingStagesRoles = await Get(stagesRolesId);
                if (existingStagesRoles != null)
                {
                    _context.StagesRoles.Remove(existingStagesRoles);
                    _context.SaveChangesAsync();
                    return ("حذف با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف وجود ندارد.", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<StagesRoles> Get(Guid stagesRolesId)
        {
            try
            {
                var model = await _context.StagesRoles.FirstOrDefaultAsync(x => x.Id == stagesRolesId);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<StagesRoles>> GetAll()
        {
            try
            {
                return await _context.StagesRoles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccss)> Update(StagesRoles stagesRoles)
        {
            try
            {
                var existingStagesRoles = await Get(stagesRoles.Id);
                if (existingStagesRoles != null)
                {

                    existingStagesRoles.CanEdit = stagesRoles.CanEdit;
                    existingStagesRoles.StageId = stagesRoles.StageId;
                    existingStagesRoles.RoleId = stagesRoles.RoleId;
                    await _context.SaveChangesAsync();
                    return ("ویرایش با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
