using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.HowToPays;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class HowToPayCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="howToPay"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, HowToPay howToPay)
        {
            try
            {
                var model = _appDbContext.HowToPay.Where(x => x.Title == howToPay.Title && x.ID != howToPay.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="howToPayId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid howToPayId)
        {
            try
            {
                var model = _appDbContext.Payments.Where(x => x.HowToPayId == howToPayId ).FirstOrDefault();
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
