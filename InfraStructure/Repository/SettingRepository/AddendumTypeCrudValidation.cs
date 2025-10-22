using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.AddendumTypes;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal static class AddendumTypeCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="addentype"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, AddendumType addentype)
        {
            try
            {
                var model = _appDbContext.AddendumTypes.Where(x => x.Title == addentype.Title && x.ID != addentype.ID).FirstOrDefault();
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
        /// <param name="addentype"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid addentype)
        {
            try
            {
                var model = _appDbContext.ContractAddendums.FirstOrDefault(x=>x.AddendumTypeId == addentype && x.IsDeleted == false);
                if (model == null)   return false; 
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
