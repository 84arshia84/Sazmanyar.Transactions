using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class ReleaseConditionCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="activitycenterTitle"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, ReleaseCondition releaseCondition)
        {
            try
            {
                var model = _appDbContext.ReleaseConditions.Where(x => x.Title == releaseCondition.Title && x.ID != releaseCondition.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="releaseConditionId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid releaseConditionId)
        {
            try
            {
                var model = _appDbContext.ContractGuarantees.Where(x => x.ReleaseConditionId == releaseConditionId).FirstOrDefault();
                if (model == null) { return false; }
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }
    }
}
