using AppCore.Entities.SettingEntities.ReasonForTerminations;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class ReasonForTerminationRepository : IReasonForTerminationRepository
    {
       
        private readonly AppDbContext _context;
        public ReasonForTerminationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert reasonfortermination in database
        /// </summary>
        /// <param name="reasonfortermination"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(ReasonForTermination reasonfortermination)
        {
            try
            {
                if (!ReasonForTerminationCrudValidation.CheckDuplicate(_context, reasonfortermination))
                {
                    var result = _context.ReasonForTerminations.AddAsync(reasonfortermination);
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
        /// Delete  reasonfortermination in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!ReasonForTerminationCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingreasonfortermination = await Get(id);
                    if (existingreasonfortermination != null)
                    {
                        existingreasonfortermination.IsDeleted = true;
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
        ///  Get ReasonForTermination by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReasonForTermination</returns>
        public async Task<ReasonForTermination> Get(Guid id)
        {
            try
            {
                var model = _context.ReasonForTerminations.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all ReasonForTermination from database
        /// </summary>
        /// <returns>List<ReasonForTermination></returns>
        /// <exception cref="List<ReasonForTermination>"></exception>
        public async Task<List<ReasonForTermination>> GetAll()
        {
            try
            {
                var models = await _context.ReasonForTerminations.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<ReasonForTermination>();
            }
        }
        /// <summary>
        /// Update ReasonForTermination from database
        /// </summary>
        /// <param name="reasonfortermination"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(ReasonForTermination reasonfortermination)
        {
            try
            {
                var existingreasonfortermination = await Get(reasonfortermination.ID);
                if (existingreasonfortermination != null)
                {
                    if (!ReasonForTerminationCrudValidation.CheckDuplicate(_context, reasonfortermination))
                    {
                        existingreasonfortermination.IsDeleted = false;
                        existingreasonfortermination.Title = reasonfortermination.Title;
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
