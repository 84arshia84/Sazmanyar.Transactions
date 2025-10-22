using AppCore.Entities.SettingEntities.ReasonForCancellations;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class ReasonForCancellationRepository : IReasonForCancellationRepository
    {
     
        private readonly AppDbContext _context;
        public ReasonForCancellationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert reasonforcancellation in database
        /// </summary>
        /// <param name="reasonforcancellation"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(ReasonForCancellation reasonforcancellation)
        {
            try
            {
                if (!ReasonForCancellationCrudValidation.CheckDuplicate(_context, reasonforcancellation))
                {
                    var result = _context.ReasonForCancellations.AddAsync(reasonforcancellation);
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
        /// Delete  reasonforcancellation in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!ReasonForCancellationCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingreasonforcancellation = await Get(id);
                    if (existingreasonforcancellation != null)
                    {
                        existingreasonforcancellation.IsDeleted = true;
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
        ///  Get ReasonForCancellation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReasonForCancellation</returns>
        public async Task<ReasonForCancellation> Get(Guid id)
        {
            try
            {
                var model = _context.ReasonForCancellations.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all ReasonForCancellation from database
        /// </summary>
        /// <returns>List<ReasonForCancellation></returns>
        /// <exception cref="List<ReasonForCancellation>"></exception>
        public async Task<List<ReasonForCancellation>> GetAll()
        {
            try
            {
                var models = await _context.ReasonForCancellations.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<ReasonForCancellation>();
            }
        }
        /// <summary>
        /// Update ReasonForCancellation from database
        /// </summary>
        /// <param name="reasonforcancellation"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(ReasonForCancellation reasonforcancellation)
        {
            try
            {
                var existingreasonforcancellation = await Get(reasonforcancellation.ID);
                if (existingreasonforcancellation != null)
                {
                    if (!ReasonForCancellationCrudValidation.CheckDuplicate(_context, reasonforcancellation))
                    {
                        existingreasonforcancellation.IsDeleted = false;
                        existingreasonforcancellation.Title = reasonforcancellation.Title;
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
