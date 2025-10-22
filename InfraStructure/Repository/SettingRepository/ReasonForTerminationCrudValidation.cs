using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class ReasonForTerminationCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="reasonForTermination"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, ReasonForTermination reasonForTermination)
        {
            try
            {
                var model = _appDbContext.ReasonForTerminations.Where(x => x.Title == reasonForTermination.Title && x.ID != reasonForTermination.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="reasonForTerminationId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid reasonForTerminationId)
        {
            return false;
        }
    }
}
