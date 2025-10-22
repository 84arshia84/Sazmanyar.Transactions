using AppCore.Entities.SettingEntities.DefaultCoefficients;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal static class DefaultCoefficientsCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="defaultCoefficients"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, DefaultCoefficients defaultCoefficients)
        {
            try
            {
                var model = _appDbContext.DefaultCoefficients.Where(x => x.Title == defaultCoefficients.Title && x.Id != defaultCoefficients.Id).FirstOrDefault();
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
        /// <param name="defaultCoefficients"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid defaultCoefficients)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
