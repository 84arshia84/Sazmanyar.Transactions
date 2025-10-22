using AppCore.Entities.SettingEntities.Activitycenters;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public static class ActivitycenterCrudValidation
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="activitycenterTitle"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, Activitycenter activitycenter)
        {
            try
            {
                var model = _appDbContext.Activitycenters.Where(x => x.Title == activitycenter.Title && x.ID!=activitycenter.ID && x.IsDeleted==false).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string s = ex.InnerException == null ? "" : ex.InnerException.Message == null ? "" : ex.InnerException.Message;
                //System.IO.File.WriteAllLines(@"C:\Samix\Eyvazkhani\Errors\CheckDuplicate.txt", new string[] { ex.Message, s });
                return true;
            }
          
        }
        /// <summary>
        /// بررسی کن ببین از این مورد در قراردادی استفاده شده 
        /// اگر شده اجازه حذف نده
        /// </summary>
        /// <param name="activitycenterId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid activitycenterId)
        {
            try
            {
                var model = _appDbContext.ServiceExplanations.Where(x => x.ActivityCenterID == activitycenterId && x.IsDeleted==false).FirstOrDefault();
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
