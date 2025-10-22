using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;
        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// افزودن ارز
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Add(Currency currency)
        {
            try
            {
                if (!CurrencyCrudValidation.CheckDuplicate(_context, currency))
                {
                    var result = _context.Currencies.AddAsync(currency);
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
                throw ex;
            }
        }
        /// <summary>
        /// حذف ارز
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Delete(Guid currencyId)
        {
            try
            {
                if (!CurrencyCrudValidation.CheckIsItUsed(_context, currencyId))
                {
                    var existingCurrency = await Get(currencyId);
                    if (existingCurrency != null)
                    {
                        _context.Currencies.Remove(existingCurrency);
                        await _context.SaveChangesAsync();
                        return ("حذف با موفقیت انجام شد.", true);
                    }
                    return ("مورد مد نظر جهت حذف وجود ندارد.", false);
                }
                return ("این مورد قبلا در قراردادی استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// گرفت ارز
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Currency> Get(Guid currencyId)
        {
            try
            {
                var model=await _context.Currencies.FirstOrDefaultAsync(x=>x.ID==currencyId);
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی ارز ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Currency>> GetAll()
        {
            try
            {
                return await _context.Currencies.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
        /// <summary>
        /// بروز رسانی ارز ها
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Update(Currency currency)
        {
            try
            {
                var existingCurrency = await Get(currency.ID);
                if (existingCurrency != null)
                {
                    if (!CurrencyCrudValidation.CheckDuplicate(_context, currency))
                    {
                        existingCurrency.IsDeleted = false;
                        existingCurrency.Title = currency.Title;
                        await _context.SaveChangesAsync();
                        return ("ویرایش با موفقیت انجام شد.", true);
                    }
                    return ("این عنوان وجود دارد.", false);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
