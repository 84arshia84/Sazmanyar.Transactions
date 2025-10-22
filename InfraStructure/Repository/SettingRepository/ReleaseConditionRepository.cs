using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class ReleaseConditionRepository : IReleaseConditionRepository
    {
 
        private readonly AppDbContext _context;
        public ReleaseConditionRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert releasecondition in database
        /// </summary>
        /// <param name="releasecondition"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(ReleaseCondition releasecondition)
        {
            try
            {
                if (!ReleaseConditionCrudValidation.CheckDuplicate(_context, releasecondition))
                {
                    var result = _context.ReleaseConditions.AddAsync(releasecondition);
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
        /// Delete  releasecondition in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!ReleaseConditionCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingreleasecondition = await Get(id);
                    if (existingreleasecondition != null)
                    {
                        existingreleasecondition.IsDeleted = true;
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
        ///  Get ReleaseCondition by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReleaseCondition</returns>
        public async Task<ReleaseCondition> Get(Guid id)
        {
            try
            {
                var model = _context.ReleaseConditions.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all ReleaseCondition from database
        /// </summary>
        /// <returns>List<ReleaseCondition></returns>
        /// <exception cref="List<ReleaseCondition>"></exception>
        public async Task<List<ReleaseCondition>> GetAll()
        {
            try
            {
                var models = await _context.ReleaseConditions.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<ReleaseCondition>();
            }
        }
        /// <summary>
        /// Update ReleaseCondition from database
        /// </summary>
        /// <param name="releasecondition"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(ReleaseCondition releasecondition)
        {
            try
            {
                var existingreleasecondition = await Get(releasecondition.ID);
                if (existingreleasecondition != null)
                {
                    if (!ReleaseConditionCrudValidation.CheckDuplicate(_context, releasecondition))
                    {
                        existingreleasecondition.IsDeleted = false;
                        existingreleasecondition.Title = releasecondition.Title;
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
