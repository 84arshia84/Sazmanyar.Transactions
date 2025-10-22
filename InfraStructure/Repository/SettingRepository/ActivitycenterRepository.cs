using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.User;
using Dapper;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    /// <summary>
    /// Activitycenter repository
    /// </summary>
    public class ActivitycenterRepository : IActivitycenterRepository
    {
        private readonly AppDbContext _context;
        public ActivitycenterRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert activitycenter in database
        /// </summary>
        /// <param name="activitycenter"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string,bool>> Add(Activitycenter activitycenter)
        {
            try
            {
                if(!ActivitycenterCrudValidation.CheckDuplicate(_context, activitycenter))
                {
                    var result = _context.Activitycenters.AddAsync(activitycenter);
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
        /// Delete  activitycenter in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!ActivitycenterCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingActivityCenter = await Get(id); 
                    if (existingActivityCenter != null)
                    {
                        existingActivityCenter.IsDeleted = true;
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
        ///  Get Activitycenter by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Activitycenter</returns>
        public async Task<Activitycenter> Get(Guid id)
        {
            try
            {
                var model =_context.Activitycenters.Where(x=>x.ID== id && x.IsDeleted== false).FirstOrDefault();
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
        /// Get all Activitycenter from database
        /// </summary>
        /// <returns>List<Activitycenter></returns>
        /// <exception cref="List<Activitycenter>"></exception>
        public async Task<List<Activitycenter>> GetAll(string query ,string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                var datas = await dbConnection.QueryAsync<Activitycenter>(query);
                var result = datas.ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<Activitycenter>();
            }
        }
        /// <summary>
        /// Update Activitycenter from database
        /// </summary>
        /// <param name="activitycenter"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(Activitycenter activitycenter)
        {
            try
            {
                var existingActivityCenter = await Get(activitycenter.ID);
                if (existingActivityCenter != null)
                {
                    if (!ActivitycenterCrudValidation.CheckDuplicate(_context, activitycenter))
                    {
                        existingActivityCenter.IsDeleted = false;
                        existingActivityCenter.Title = activitycenter.Title;
                        existingActivityCenter.Code = activitycenter.Code;
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
