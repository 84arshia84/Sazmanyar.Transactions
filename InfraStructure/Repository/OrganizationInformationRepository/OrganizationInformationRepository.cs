using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.UnitOfWork;
using InfraStructure.Repository.SettingRepository;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.OrganizationInformationRepository
{
    public class OrganizationInformationRepository : IOrganizationInformationRepository
    {
        private readonly AppDbContext _context;
        public OrganizationInformationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess)> Add(OrganizationInformation organizationInformation)
        {
            try
            {
                if (!OrganizationInformationCRUDValidations.CheckDuplicate(_context, organizationInformation))
                {

                    var result = _context.OrganizationInformations.AddAsync(organizationInformation);
                    await _context.SaveChangesAsync();
                    if (result.IsCompleted)
                    {
                        return ("ثبت با موفقیت انجام شد.", true);
                    }
                    return ("ثبت با خطا روبرو شد.", false);
                }
                return ("اجازه ثبت مجدد ندارید.", false);
            }
            catch (Exception ex)
            {
                return ("ثبت با خطا روبرو شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddAcount(Account account)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                return ("ثبت موفقیت آمیز بود", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(string message, bool isSuccess)> DeleteAccount(Guid id)
        {
            try
            {
                var model = await _context.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    if (!OrganizationInformationCRUDValidations.CheckIsItUsedAccount(_context, id))
                    {
                        _context.Accounts.Remove(model);
                        return ("باموفقیت حذف شد", true);
                    }
                    return ("این حساب قبلا در صورت وضعیتی استفاده شده", false);
                }
                return ("حذف با خطا مواجه شد", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        ///  Get CorespondentLegal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CorespondentLegal</returns>
        public async Task<OrganizationInformation> Get()
        {
            try
            {
                var model = _context.OrganizationInformations.FirstOrDefault();
                if (model == null)
                {
                    return null;
                }
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Account>> GetAccounts()
        {
            try
            {
                return await _context.Accounts.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update CorespondentLegal from database
        /// </summary>
        /// <param name="corespondentlegal"></param>
        /// <returns></returns>
        public async Task<(string message, bool isSuccess)> Update(OrganizationInformation organizationInformation)
        {
            try
            {
                var existingorganizationInformation = await Get();
                if (existingorganizationInformation != null)
                {
                    if (!OrganizationInformationCRUDValidations.CheckDuplicate(_context, organizationInformation))
                    {
                        existingorganizationInformation.Address = organizationInformation.Address;
                        existingorganizationInformation.RegistrationNumber = organizationInformation.RegistrationNumber;
                        existingorganizationInformation.CompanyName = organizationInformation.CompanyName;
                        existingorganizationInformation.NationalID = organizationInformation.NationalID;
                        existingorganizationInformation.CityId = organizationInformation.CityId;
                        existingorganizationInformation.ProvincId = organizationInformation.ProvincId;
                        existingorganizationInformation.CountyId = organizationInformation.CountyId;
                        existingorganizationInformation.PhoneNumber = organizationInformation.PhoneNumber;
                        existingorganizationInformation.Email = organizationInformation.Email;
                        existingorganizationInformation.PostalCode = organizationInformation.PostalCode;


                        await _context.SaveChangesAsync();
                        return ("ویرایش با موفقیت انجام شد.", true);
                    }
                    return ("این عنوان وجود دارد.", false);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception)
            {
                return ("ویرایش با خطا روبرو شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> UpdateAccount(Account account)
        {
            try
            {
                var model = await _context.Accounts.Where(x => x.Id == account.Id).FirstOrDefaultAsync();
                if (model != null)
                {
                    if (!OrganizationInformationCRUDValidations.CheckDuplicate(_context, account))
                    {
                        _context.Entry(model).CurrentValues.SetValues(account);
                        return ("با موفقیت ویرایش شد", true);
                    }
                    return ("حساب تکراری است", false);
                }
                return ("مورد مد نظر یافت نشد", false);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
