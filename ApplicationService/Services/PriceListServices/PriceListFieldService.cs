using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.PriceListMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.PriceLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.PriceListServices
{
    internal class PriceListFieldService : IPriceListFieldService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;

        public PriceListFieldService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        /// <summary>
        /// سرویس 
        /// </summary>
        /// <param name="cource"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(Guid ID, string message, bool isSecces)> Add(PriceListFieldDto field)
        {
            try
            {
                var model = PriceListFieldAutoMapperProfile.DtoToEntity(field, _errorLoggerService);
                model.ID = Guid.NewGuid();
                var result=await _unitOfWork.PriceListFieldRepository.Add(model);
                return result;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (Guid.Empty,"خطا در سرویس افزودن رشته",false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cources"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(string message, bool isSecces)> AddList(List<PriceListFieldDto> cources)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSecces)> Delete(Guid fieldId)
        {
            try
            {
                return await _unitOfWork.PriceListFieldRepository.Delete(fieldId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در سرویس حذف رشته", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PriceListField> Get(Guid fieldId)
        {
            try
            {
                return await  _unitOfWork.PriceListFieldRepository.Get(fieldId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new PriceListField();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListField>> GetAll()
        {
            try
            {
                return await _unitOfWork.PriceListFieldRepository.GetAll();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceListField>();
            }
        }
        /// <summary>
        /// رشته های یک سال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListFieldDto>> GetAllByYearId(Guid id)
        {
            try
            {
                return PriceListFieldAutoMapperProfile.EntitesToDtos(await _unitOfWork.PriceListFieldRepository.GetAllByYearId(id),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceListFieldDto>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSecces)> Update(PriceListFieldDto field)
        {
            try
            {
                return await _unitOfWork.PriceListFieldRepository.Update(PriceListFieldAutoMapperProfile.DtoToEntity(field, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی رشته", false);
            }
        }
    }
}
