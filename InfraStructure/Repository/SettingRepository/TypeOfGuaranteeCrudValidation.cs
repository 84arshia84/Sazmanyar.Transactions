using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class TypeOfGuaranteeCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="typeOfGuarantee"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, TypeOfGuarantee typeOfGuarantee)
        {
            try
            {
                var model = _appDbContext.TypeOfGuarantees.Where(x => x.Title == typeOfGuarantee.Title && x.ID != typeOfGuarantee.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="typeOfGuaranteeId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid typeOfGuaranteeId)
        {
            try
            {
                var model = _appDbContext.ContractGuarantees.Where(x=>x.TypeOfGuaranteeId == typeOfGuaranteeId && x.IsDeleted == false).FirstOrDefault();
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
