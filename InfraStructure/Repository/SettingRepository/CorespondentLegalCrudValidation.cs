using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class CorespondentLegalCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="corespondentLegal"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, CorespondentLegal corespondentLegal)
        {
            try
            {
                var model = _appDbContext.CorespondentLegals.Where(x => x.CompanyName == corespondentLegal.CompanyName && x.ID != corespondentLegal.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="corespondentLegalId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid corespondentLegalId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x => x.CorespondentID == corespondentLegalId && x.IsDeleted == false).FirstOrDefault();
                if(model == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
