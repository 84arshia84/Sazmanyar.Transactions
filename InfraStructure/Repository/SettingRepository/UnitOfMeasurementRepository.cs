using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class UnitOfMeasurementRepository : IUnitOfMeasurementRepository
    {
        private readonly AppDbContext _context;
        public UnitOfMeasurementRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// افزودن واحد اندازه گیری
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Add(UnitOfMeasurement unit)
        {
            try
            {
                if (!UnitOfMeasurementCrudValidation.CheckDuplicate(_context, unit))
                {
                    var result = _context.UnitOfMeasurements.AddAsync(unit);
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
        /// حذ ف کردن واحد اندازه گیری
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Delete(Guid unitId)
        {
            try
            {
                if (!UnitOfMeasurementCrudValidation.CheckIsItUsed(_context, unitId))
                {
                    var existingUnit = await Get(unitId);
                    if (existingUnit != null)
                    {
                        _context.UnitOfMeasurements.Remove(existingUnit);
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
        /// گرفتن واحد اندازه گیری
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UnitOfMeasurement> Get(Guid unitId)
        {
            try
            {
                var model = await _context.UnitOfMeasurements.FirstOrDefaultAsync(x => x.ID == unitId);
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی واحد های اندازه گیری
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<UnitOfMeasurement>> GetAll()
        {
            try
            {
                return await _context.UnitOfMeasurements.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// بروزرسانی واحد اندازه گیری
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccss)> Update(UnitOfMeasurement unit)
        {
            try
            {
                var existingUnit = await Get(unit.ID);
                if (existingUnit != null)
                {
                    if (!UnitOfMeasurementCrudValidation.CheckDuplicate(_context, unit))
                    {
                        existingUnit.IsDeleted = false;
                        existingUnit.Title = unit.Title;
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
