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
    internal class TypeOfCooperationService:ITypeOfCooperationService
    {
        private IUnitOfWork _unitOfWork;
        public TypeOfCooperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن نوع همکاری به دیتابیس
        /// </summary>
        /// <param name="typeOfCooperationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(TypeOfCooperationDto typeOfCooperationDto)
        {
            try
            {
                typeOfCooperationDto.key = Guid.NewGuid();
                var model = TypeOfCooperationAutoMapperProfile.DtoToEntity(typeOfCooperationDto);
                return await _unitOfWork.TypeOfCooperationRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  نوع همکاری 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.TypeOfCooperationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام نوع همکاری ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<TypeOfCooperationDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.TypeOfCooperationRepository.GetAll();
                return TypeOfCooperationAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<TypeOfCooperationDto>();
            }
        }
        /// <summary>
        /// بروزرسانی نوع همکاری 
        /// </summary>
        /// <param name="typeOfCooperationDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(TypeOfCooperationDto typeOfCooperationDto)
        {
            try
            {
                var model = TypeOfCooperationAutoMapperProfile.DtoToEntity(typeOfCooperationDto);
                return await _unitOfWork.TypeOfCooperationRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
