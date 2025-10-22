using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class BasisForStartingTheProjectRepository : IBasisForStartingTheProjectRepository
    {

        private readonly AppDbContext _context;
        public BasisForStartingTheProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert basisforstartingtheproject in database
        /// </summary>
        /// <param name="basisforstartingtheproject"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(BasisForStartingTheProject basisforstartingtheproject)
        {
            try
            {
                if (!BasisForStartingTheProjectCrudValidation.CheckDuplicate(_context, basisforstartingtheproject))
                {
                    var result = _context.BasisForStartingTheProjects.AddAsync(basisforstartingtheproject);
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
        /// Delete  basisforstartingtheproject in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!BasisForStartingTheProjectCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingBasisForStartingTheProjects = await Get(id);
                    if (existingBasisForStartingTheProjects != null)
                    {
                        existingBasisForStartingTheProjects.IsDeleted = true;
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
        ///  Get BasisForStartingTheProject by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BasisForStartingTheProject</returns>
        public async Task<BasisForStartingTheProject> Get(Guid id)
        {
            try
            {
                var model = _context.BasisForStartingTheProjects.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all BasisForStartingTheProject from database
        /// </summary>
        /// <returns>List<BasisForStartingTheProject></returns>
        /// <exception cref="List<BasisForStartingTheProject>"></exception>
        public async Task<List<BasisForStartingTheProject>> GetAll()
        {
            try
            {
                var models = await _context.BasisForStartingTheProjects.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<BasisForStartingTheProject>();
            }
        }
        /// <summary>
        /// Update BasisForStartingTheProject from database
        /// </summary>
        /// <param name="basisforstartingtheproject"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(BasisForStartingTheProject basisforstartingtheproject)
        {
            try
            {
                var existingBasisForStartingTheProjects = await Get(basisforstartingtheproject.ID);
                if (existingBasisForStartingTheProjects != null)
                {
                    if (!BasisForStartingTheProjectCrudValidation.CheckDuplicate(_context, basisforstartingtheproject))
                    {
                        existingBasisForStartingTheProjects.IsDeleted = false;
                        existingBasisForStartingTheProjects.Title = basisforstartingtheproject.Title;
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
