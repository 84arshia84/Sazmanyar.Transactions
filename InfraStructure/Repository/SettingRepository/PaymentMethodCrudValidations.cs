using AppCore.Entities.SettingEntities.PaymentMethods;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal static class PaymentMethodCrudValidations
    {
        //public static bool CheckDuplicate(AppDbContext _appDbContext, PaymentMethod currency)
        //{
        //    try
        //    {
        //        var model = _appDbContext.Activitycenters.Where(x => x.Title == currency.Title && x.ID != currency.ID && x.IsDeleted == false).FirstOrDefault();
        //        if (model == null)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.InnerException == null ? "" : ex.InnerException.Message == null ? "" : ex.InnerException.Message;
        //        System.IO.File.WriteAllLines(@"C:\Samix\Eyvazkhani\Errors\CheckDuplicate.txt", new string[] { ex.Message, s });
        //        return true;
        //    }
        //}
        //public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid activitycenterId)
        //{
        //    return false;
        //}
    }
}
