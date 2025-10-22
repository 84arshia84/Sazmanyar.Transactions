using AppCore.Entities.SettingEntities.PaymentMethods;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;
        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccss)> Add(PaymentMethod paymentMethod)
        {
            try
            {
                //if (!PaymentMethodCrudValidation.CheckDuplicate(_context, paymentMethod))
                //{
                var result = _context.PaymentMethods.AddAsync(paymentMethod);
                await _context.SaveChangesAsync();
                if (result.IsCompleted)
                {
                    return ("ثبت با موفقیت انجام شد.", true);
                }
                return ("ثبت با خطا روبرو شد.", false);
                //}
                //return ("اجازه ثبت مجدد ندارید.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccss)> Delete(Guid paymentMethodId)
        {
            try
            {
                //if (!PaymentMethodCrudValidation.CheckIsItUsed(_context, paymentMethodId))
                //{
                var existingPaymentMethod = await Get(paymentMethodId);
                if (existingPaymentMethod != null)
                {
                    _context.PaymentMethods.Remove(existingPaymentMethod);
                    await _context.SaveChangesAsync();
                    return ("حذف با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف وجود ندارد.", false);
                //}
                //return ("این مورد قبلا در قراردادی استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PaymentMethod> Get(Guid paymentMethodId)
        {
            try
            {
                var model = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.ID == paymentMethodId);
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<PaymentMethod>> GetAll()
        {
            try
            {
                return await _context.PaymentMethods.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(string message, bool isSuccss)> Update(PaymentMethod paymentMethod)
        {
            try
            {
                var existingPaymentMethod = await Get(paymentMethod.ID);
                if (existingPaymentMethod != null)
                {
                    //if (!PaymentMethodCrudValidation.CheckDuplicate(_context, paymentMethod))
                    //{
                    existingPaymentMethod.Title = paymentMethod.Title;
                    await _context.SaveChangesAsync();
                    return ("ویرایش با موفقیت انجام شد.", true);
                    //}
                    //return ("این عنوان وجود دارد.", false);
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
