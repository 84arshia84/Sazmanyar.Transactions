using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class BasisFortheEndOftheProjectRepository : IBasisFortheEndOftheProjectRepository
    {

        private readonly AppDbContext _context;
        public BasisFortheEndOftheProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert basisfortheendoftheproject in database
        /// </summary>
        /// <param name="basisfortheendoftheproject"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(BasisFortheEndOftheProject basisfortheendoftheproject)
        {
            try
            {
                if (!BasisFortheEndOftheProjectCrudValidation.CheckDuplicate(_context, basisfortheendoftheproject))
                {
                    var result = _context.BasisFortheEndOftheProjects.AddAsync(basisfortheendoftheproject);
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
        /// Delete  basisfortheendoftheproject in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!BasisFortheEndOftheProjectCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingBasisForendingTheProjects = await Get(id);
                    if (existingBasisForendingTheProjects != null)
                    {
                        existingBasisForendingTheProjects.IsDeleted = true;
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
        ///  Get BasisFortheEndOftheProject by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BasisFortheEndOftheProject</returns>
        public async Task<BasisFortheEndOftheProject> Get(Guid id)
        {
            try
            {
                var model = _context.BasisFortheEndOftheProjects.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all BasisFortheEndOftheProject from database
        /// </summary>
        /// <returns>List<BasisFortheEndOftheProject></returns>
        /// <exception cref="List<BasisFortheEndOftheProject>"></exception>
        public async Task<List<BasisFortheEndOftheProject>> GetAll()
        {
            try
            {
                var models = await _context.BasisFortheEndOftheProjects.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<BasisFortheEndOftheProject>();
            }
        }
        /// <summary>
        /// Update BasisFortheEndOftheProject from database
        /// </summary>
        /// <param name="basisfortheendoftheproject"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(BasisFortheEndOftheProject basisfortheendoftheproject)
        {
            try
            {
                var existingBasisForendingTheProjects = await Get(basisfortheendoftheproject.ID);
                if (existingBasisForendingTheProjects != null)
                {
                    if (!BasisFortheEndOftheProjectCrudValidation.CheckDuplicate(_context, basisfortheendoftheproject))
                    {
                        existingBasisForendingTheProjects.IsDeleted = false;
                        existingBasisForendingTheProjects.Title = basisfortheendoftheproject.Title;
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
