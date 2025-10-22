using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal static class CurrencyCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="activitycenterTitle"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, Currency currency)
        {
            try
            {
                var model = _appDbContext.Currencies.Where(x => x.Title == currency.Title && x.ID != currency.ID && x.IsDeleted == false).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string s = ex.InnerException == null ? "" : ex.InnerException.Message == null ? "" : ex.InnerException.Message;
                System.IO.File.WriteAllLines(@"C:\Samix\Eyvazkhani\Errors\CheckDuplicate.txt", new string[] { ex.Message, s });
                return true;
            }

        }
        /// <summary>
        /// بررسی کن ببین از این مورد در قراردادی استفاده شده 
        /// اگر شده اجازه حذف نده
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid currencyId)
        {
            try
            {
                var model = _appDbContext.ServiceExplanations.Where(x=>x.CurrencyID == currencyId && x.IsDeleted == false).FirstOrDefault();
                if (model == null) { return false;}
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
