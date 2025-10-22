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
    internal class ReleaseConditionService:IReleaseConditionService
    {
        private IUnitOfWork _unitOfWork;
        public ReleaseConditionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن شرط آزادسازی به دیتابیس
        /// </summary>
        /// <param name="releaseConditionDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(ReleaseConditionDto releaseConditionDto)
        {
            try
            {
                releaseConditionDto.key = Guid.NewGuid();
                var model = ReleaseConditionAutoMapperProfile.DtoToEntity(releaseConditionDto);
                return await _unitOfWork.ReleaseConditionRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  شرط آزادسازی 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.ReleaseConditionRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام شرط آزادسازی ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReleaseConditionDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.ReleaseConditionRepository.GetAll();
                return ReleaseConditionAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<ReleaseConditionDto>();
            }
        }
        /// <summary>
        /// بروزرسانی شرط آزادسازی 
        /// </summary>
        /// <param name="releaseConditionDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(ReleaseConditionDto releaseConditionDto)
        {
            try
            {
                var model = ReleaseConditionAutoMapperProfile.DtoToEntity(releaseConditionDto);
                return await _unitOfWork.ReleaseConditionRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
