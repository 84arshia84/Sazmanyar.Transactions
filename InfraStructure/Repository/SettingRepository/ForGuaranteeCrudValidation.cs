using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.ForGuarantees;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class ForGuaranteeCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="forGuarantee"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, ForGuarantee forGuarantee)
        {
            try
            {
                var model = _appDbContext.ForGuarantees.Where(x => x.Title == forGuarantee.Title && x.ID != forGuarantee.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="forGuaranteeId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid forGuaranteeId)
        {
            try
            {
                var model = _appDbContext.ContractGuarantees.Where(x => x.ForGuaranteeId == forGuaranteeId ).FirstOrDefault();
                if(model == null) { return false; }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
