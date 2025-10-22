using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.CheckLists;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class CheckListCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="activitycenterTitle"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, CheckList checkList)
        {
            try
            {
                var model = _appDbContext.CheckLists.Where(x => x.Title == checkList.Title && x.Id != checkList.Id && x.ContractTypeId!=checkList.ContractTypeId).FirstOrDefault();
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
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid contractTypeId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x => x.ContractTypeID == contractTypeId && x.IsDeleted == false).FirstOrDefault();
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
