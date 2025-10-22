using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.OrganizationInformationRepository
{
    public class OrganizationInformationCRUDValidations
    {
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="corespondentLegal"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, OrganizationInformation organizationInformation)
        {
            try
            {
                var model = _appDbContext.OrganizationInformations.Where(x => x.CompanyName == organizationInformation.CompanyName && x.ID != organizationInformation.ID ).FirstOrDefault();
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
        public static bool CheckDuplicate(AppDbContext _appDbContext, Account account)
        {
            try
            {
                var model = _appDbContext.Accounts.Where(x => x.AccountNumber == account.AccountNumber && x.Id != account.Id).FirstOrDefault();
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
        /// <param name="corespondentLegalId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appdbcontext, Guid organizationinformationid)
        {
            try
            {
                var model = _appdbcontext.Accounts.Where(x => x.OrganizationInformationId == organizationinformationid && x.IsActive == true).FirstOrDefault();
                if (model == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public static bool CheckIsItUsedAccount(AppDbContext _appdbcontext, Guid accountId)
        {
            try
            {
                var model = _appdbcontext.InvoiceBaseInformations.Where(x => x.AccountId == accountId).FirstOrDefault();
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

