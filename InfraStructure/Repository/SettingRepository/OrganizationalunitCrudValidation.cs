using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Organizationalunits;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class OrganizationalunitCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="organizationalunit"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, Organizationalunit organizationalunit)
        {
            try
            {
                var model = _appDbContext.Organizationalunits.Where(x => x.Title == organizationalunit.Title && x.ID != organizationalunit.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="organizationalunitId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid organizationalunitId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x => x.OrganizationUnitID == organizationalunitId && x.IsDeleted == false).FirstOrDefault();
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
