using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class RoleOfOrganizationCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="roleOfOrganization"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, RoleOfOrganization roleOfOrganization)
        {
            try
            {
                var model = _appDbContext.RoleOfOrganizations.Where(x => x.Title == roleOfOrganization.Title && x.ID != roleOfOrganization.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="roleOfOrganizationId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid roleOfOrganizationId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x => x.RoleOFOrganizationID == roleOfOrganizationId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
