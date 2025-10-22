using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Entities.SettingEntities.TransActionTypes;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class TransActionTypeRepository : ITransActionTypeRepository
    {
      
        private readonly AppDbContext _context;
        public TransActionTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert transactiontype in database
        /// </summary>
        /// <param name="transactiontype"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(TransActionType transactiontype)
        {
            try
            {
                if (!TransActionTypeCrudValidation.CheckDuplicate(_context, transactiontype))
                {
                    var result = _context.TransActionTypes.AddAsync(transactiontype);
                    await _context.SaveChangesAsync();
                    if (result.IsCompleted)
                    {
                        return Tuple.Create("ثبت با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("ثبت با خطا روبرو شد.", false);
                }
                return Tuple.Create("اجازه ثبت مجدد ندارید.", false);
            }
            catch (Exception ex)
            {
                return Tuple.Create("ثبت با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        /// Delete  transactiontype in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!TransActionTypeCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingtransactiontype = await Get(id);
                    if (existingtransactiontype != null)
                    {
                        existingtransactiontype.IsDeleted = true;
                        await _context.SaveChangesAsync();
                        return Tuple.Create("حذف با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("مورد مد نظر جهت حذف وجود ندارد.", false);
                }
                return Tuple.Create("این مورد قبلا در قراردادی استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                return Tuple.Create("حذف با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        ///  Get TransActionType by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TransActionType</returns>
        public async Task<TransActionType> Get(Guid id)
        {
            try
            {
                var model = _context.TransActionTypes.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// <summary>
        /// Get all TransActionType from database
        /// </summary>
        /// <returns>List<TransActionType></returns>
        /// <exception cref="List<TransActionType>"></exception>
        public async Task<List<TransActionType>> GetAll()
        {
            try
            {
                var models = await _context.TransActionTypes.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<TransActionType>();
            }
        }
        /// <summary>
        /// Update TransActionType from database
        /// </summary>
        /// <param name="transactiontype"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(TransActionType transactiontype)
        {
            try
            {
                var existingtransactiontype = await Get(transactiontype.ID);
                if (existingtransactiontype != null)
                {
                    if (!TransActionTypeCrudValidation.CheckDuplicate(_context, transactiontype))
                    {
                        existingtransactiontype.IsDeleted = false;
                        existingtransactiontype.Title = transactiontype.Title;
                        await _context.SaveChangesAsync();
                        return Tuple.Create("ویرایش با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("این عنوان وجود دارد.", false);
                }
                return Tuple.Create("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception)
            {
                return Tuple.Create("ویرایش با خطا روبرو شد.", false);
            }
        }
    }
}
