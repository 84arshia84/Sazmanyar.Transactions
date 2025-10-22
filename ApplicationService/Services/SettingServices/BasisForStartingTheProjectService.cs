using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    public class BasisForStartingTheProjectService : IBasisForStartingTheProjectService
    {
        private IUnitOfWork _unitOfWork;
        public BasisForStartingTheProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن مبنای شروع پروژه به دیتابیس
        /// </summary>
        /// <param name="basisForStartingTheProject"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(BasisForStartingTheProjectDto basisForStartingTheProject)
        {
            try
            {
                basisForStartingTheProject.key = Guid.NewGuid();
                var model = BasisForStartingTheProjectAutoMapperProfile.DtoToEntity(basisForStartingTheProject);
                return await _unitOfWork.BasisForStartingTheProjectRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف مبنای شروع پروژه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.BasisForStartingTheProjectRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام مبنای شروع پروژه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<BasisForStartingTheProjectDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.BasisForStartingTheProjectRepository.GetAll();
                return BasisForStartingTheProjectAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<BasisForStartingTheProjectDto>();
            }
        }
        /// <summary>
        /// بروزرسانی مبنای شروع پروژه
        /// </summary>
        /// <param name="basisForStartingTheProject"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(BasisForStartingTheProjectDto basisForStartingTheProject)
        {
            try
            {
                var model = BasisForStartingTheProjectAutoMapperProfile.DtoToEntity(basisForStartingTheProject);
                return await _unitOfWork.BasisForStartingTheProjectRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
