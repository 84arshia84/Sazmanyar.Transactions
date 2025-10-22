using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class BasisFortheEndOftheProjectCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="basisForEndingTheProject"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, BasisFortheEndOftheProject basisForEndingTheProject)
        {
            try
            {
                var model = _appDbContext.BasisFortheEndOftheProjects.Where(x => x.Title == basisForEndingTheProject.Title && x.ID != basisForEndingTheProject.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="basisForEndingTheProjectId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid basisForEndingTheProjectId)
        {
            return false;
        }
    }
}
