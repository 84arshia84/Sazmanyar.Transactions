using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.CreditSources;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class CreditSourceCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="creditSource"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, CreditSource creditSource)
        {
            try
            {
                var model = _appDbContext.CreditSources.Where(x => x.Title == creditSource.Title && x.ID != creditSource.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="creditSourceId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid creditSourceId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x=>x.CreditSourceID == creditSourceId && x.IsDeleted == false).FirstOrDefault();
                if (model == null) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
