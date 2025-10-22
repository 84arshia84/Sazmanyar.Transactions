using AppCore.Entities.SettingEntities.LookUpTables;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal static class LookUpTableCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="lookUpTable"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, LookUpTable lookUpTable)
        {
            try
            {
                var model = _appDbContext.LookUpTables.Where(x => x.Title == lookUpTable.Title && x.ID != lookUpTable.ID && x.IsDeleted == false).FirstOrDefault();
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
        public static bool CheckDuplicate(AppDbContext _appDbContext, LookUpTableInside inside)
        {
            try
            {
                var model = _appDbContext.LookUpTablesInside.Where(x => x.Title == inside.Title && x.ID != inside.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="lookuptableId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid lookuptableId)
        {
            try
            {
                var model = _appDbContext.CheckLists.Where(x => x.LookUpTableId == lookuptableId ).FirstOrDefault();
                if (model == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
