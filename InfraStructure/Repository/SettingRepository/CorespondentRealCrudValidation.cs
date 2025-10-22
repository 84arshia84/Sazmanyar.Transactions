using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class CorespondentRealCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="corespondentReal"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, CorespondentReal corespondentReal)
        {
            try
            {
                var model = _appDbContext.CorespondentReals.Where(x => x.Family == corespondentReal.Family && x.Name==corespondentReal.Name && x.ID != corespondentReal.ID && x.IsDeleted == false).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        /// <summary>
        /// بررسی کن ببین از این مورد در قراردادی استفاده شده 
        /// اگر شده اجازه حذف نده
        /// </summary>
        /// <param name="corespondentRealId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid corespondentRealId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x=>x.CorespondentID==corespondentRealId && x.IsDeleted == false).FirstOrDefault();
                if(model == null) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                return true;    
            }
        }
    }
}
