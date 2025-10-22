using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public static class BasisForStartingTheProjectCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="basisForStartingTheProject"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, BasisForStartingTheProject basisForStartingTheProject)
        {
            try
            {
                var model = _appDbContext.BasisForStartingTheProjects.Where(x => x.Title == basisForStartingTheProject.Title && x.ID != basisForStartingTheProject.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="basisForStartingTheProjectId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid basisForStartingTheProjectId)
        {
            try
            {
                var model = _appDbContext.ContratTimeProfiles.Where(x => x.BasisForStartingProjectID == basisForStartingTheProjectId).FirstOrDefault();
                if(model == null) { return false; }
                return true;
            }
            catch (Exception ex)
            {

                return true;
            }
        }
    }
}
