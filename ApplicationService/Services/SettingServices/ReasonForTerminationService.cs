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
    internal class ReasonForTerminationService:IReasonForTerminationService
    {
        private IUnitOfWork _unitOfWork;
        public ReasonForTerminationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن علت خاتمه به دیتابیس
        /// </summary>
        /// <param name="reasonForTerminationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(ReasonForTerminationDto reasonForTerminationDto)
        {
            try
            {
                reasonForTerminationDto.key = Guid.NewGuid();
                var model = ReasonForTerminationAutoMapperProfile.DtoToEntity(reasonForTerminationDto);
                return await _unitOfWork.ReasonForTerminationRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  علت خاتمه 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.ReasonForTerminationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام علت خاتمه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReasonForTerminationDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.ReasonForTerminationRepository.GetAll();
                return ReasonForTerminationAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<ReasonForTerminationDto>();
            }
        }
        /// <summary>
        /// بروزرسانی علت خاتمه 
        /// </summary>
        /// <param name="reasonForTerminationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(ReasonForTerminationDto reasonForTerminationDto)
        {
            try
            {
                var model = ReasonForTerminationAutoMapperProfile.DtoToEntity(reasonForTerminationDto);
                return await _unitOfWork.ReasonForTerminationRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
