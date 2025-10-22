using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class FinePaymentMethodCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="finePaymentMethod"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, FinePaymentMethod finePaymentMethod)
        {
            try
            {
                var model = _appDbContext.FinePaymentMethods.Where(x => x.Title == finePaymentMethod.Title && x.ID != finePaymentMethod.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="finePaymentMethodId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid finePaymentMethodId)
        {
            try
            {
                var model = _appDbContext.ServiceExplanations.Where(x=>x.FinePaymentMethodID == finePaymentMethodId && x.IsDeleted == false).FirstOrDefault(); 
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
