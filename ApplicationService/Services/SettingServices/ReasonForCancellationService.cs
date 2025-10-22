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
    internal class ReasonForCancellationService:IReasonForCancellationService
    {
        private IUnitOfWork _unitOfWork;
        public ReasonForCancellationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن علت فسخ به دیتابیس
        /// </summary>
        /// <param name="reasonForCancellationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(ReasonForCancellationDto reasonForCancellationDto)
        {
            try
            {
                reasonForCancellationDto.key = Guid.NewGuid();
                var model = ReasonForCancellationAutoMapperProfile.DtoToEntity(reasonForCancellationDto);
                return await _unitOfWork.ReasonForCancellationRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  علت فسخ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.ReasonForCancellationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام علت فسخ
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReasonForCancellationDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.ReasonForCancellationRepository.GetAll();
                return ReasonForCancellationAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<ReasonForCancellationDto>();
            }
        }
        /// <summary>
        /// بروزرسانی علت فسخ 
        /// </summary>
        /// <param name="reasonForCancellationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(ReasonForCancellationDto reasonForCancellationDto)
        {
            try
            {
                var model = ReasonForCancellationAutoMapperProfile.DtoToEntity(reasonForCancellationDto);
                return await _unitOfWork.ReasonForCancellationRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
