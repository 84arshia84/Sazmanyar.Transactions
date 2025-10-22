using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class ReasonForCancellationCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="reasonForCancellation"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, ReasonForCancellation reasonForCancellation)
        {
            try
            {
                var model = _appDbContext.ReasonForCancellations.Where(x => x.Title == reasonForCancellation.Title && x.ID != reasonForCancellation.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="reasonForCancellationId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid reasonForCancellationId)
        {
            return false;
        }
    }
}
