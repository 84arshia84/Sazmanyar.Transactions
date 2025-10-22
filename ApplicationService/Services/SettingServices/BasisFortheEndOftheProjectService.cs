using ApplicationService.DtoModels.SettingDtos;
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
    public class BasisFortheEndOftheProjectService:IBasisFortheEndOftheProjectService
    {
        private IUnitOfWork _unitOfWork;
        public BasisFortheEndOftheProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن مبنای پایان پروژه به دیتابیس
        /// </summary>
        /// <param name="basisForEndingTheProject"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(BasisFortheEndOftheProjectDto basisForEndingTheProject)
        {
            try
            {
                basisForEndingTheProject.key = Guid.NewGuid();
                var model = BasisFortheEndOftheProjectAutoMapperProfile.DtoToEntity(basisForEndingTheProject);
                return await _unitOfWork.BasisFortheEndOftheProjectRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف مبنای پایان پروژه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.BasisFortheEndOftheProjectRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام مبنای پایان پروژه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<BasisFortheEndOftheProjectDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.BasisFortheEndOftheProjectRepository.GetAll();
                return BasisFortheEndOftheProjectAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<BasisFortheEndOftheProjectDto>();
            }
        }
        /// <summary>
        /// بروزرسانی مبنای پایان پروژه
        /// </summary>
        /// <param name="basisForEndingTheProject"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(BasisFortheEndOftheProjectDto basisForEndingTheProject)
        {
            try
            {
                var model = BasisFortheEndOftheProjectAutoMapperProfile.DtoToEntity(basisForEndingTheProject);
                return await _unitOfWork.BasisFortheEndOftheProjectRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
