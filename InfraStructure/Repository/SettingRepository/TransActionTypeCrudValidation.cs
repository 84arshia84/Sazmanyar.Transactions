using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.TransActionTypes;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class TransActionTypeCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="transActionType"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, TransActionType transActionType)
        {
            try
            {
                var model = _appDbContext.TransActionTypes.Where(x => x.Title == transActionType.Title && x.ID != transActionType.ID && x.IsDeleted == false).FirstOrDefault();
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
        /// <param name="transActionTypeId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid transActionTypeId)
        {
            try
            {
                var model = _appDbContext.Contracts.Where(x => x.TransActionTypeID == transActionTypeId && x.IsDeleted == false).FirstOrDefault();
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
