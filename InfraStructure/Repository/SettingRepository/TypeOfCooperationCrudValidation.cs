using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class TypeOfCooperationCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="typeOfCooperation"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, TypeOfCooperation typeOfCooperation)
        {
            try
            {
                var model = _appDbContext.TypeOfCooperations.Where(x => x.Title == typeOfCooperation.Title && x.ID != typeOfCooperation.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="typeOfCooperationId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid typeOfCooperationId)
        {
            try
            {

                var modelLegal = _appDbContext.CorespondentLegals
                    .Include(cl => cl.CorespondAndTypeOfCoopRels)
                    .Where(cl=>cl.CorespondAndTypeOfCoopRels.
                      Any(cr=>cr.TypeOfCoopreationID==typeOfCooperationId))
                    .FirstOrDefault();

                var modelReal = _appDbContext.CorespondentReals
                    .Include(cr=>cr.CorespondAndTypeOfCoopRels)
                    .Where(cr=>cr.CorespondAndTypeOfCoopRels.
                    Any(re=>re.TypeOfCooperationID == typeOfCooperationId))
                    .FirstOrDefault();

                if (modelLegal == null && modelReal == null) { return false; }
                return true;

            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
