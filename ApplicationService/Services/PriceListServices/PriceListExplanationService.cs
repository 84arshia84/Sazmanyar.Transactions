using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.Mapper.PriceListMapper;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.PriceLists;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.PriceListServices
{
    internal class PriceListExplanationService : IPriceListExplanationService
    {
        private IErrorLoggerService _errorLoggerService;
        private IUnitOfWork _unitOfWork;
        public PriceListExplanationService(IErrorLoggerService errorLoggerService, IUnitOfWork unitOfWork)
        {
            _errorLoggerService = errorLoggerService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// افزودن شرح به یک فصل
        /// </summary>
        /// <param name="Explanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> AddExplanation(PriceListExplanationsDto Explanation)
        {
            try
            {
                var model = PriceListExplanationAutoMapperProfile.DtoToEntity(Explanation, _errorLoggerService);
                model.ID=Guid.NewGuid();
                return await _unitOfWork.PriceListExplanationRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در افزودن شرح ", false);
            }
        }
        /// <summary>
        /// افزودن تعدادی شرح به یک فصل
        /// </summary>
        /// <param name="listExplanations"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> AddListExplanations(List<PriceListExplanationsDto> listExplanations)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// حذف شرح
        /// </summary>
        /// <param name="explanationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> DeleteExplanation(Guid explanationId)
        {
            try
            {
                return await _unitOfWork.PriceListExplanationRepository.Delete(explanationId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف شرح ", false);
            }
        }
        /// <summary>
        /// گرفتن تمامی شرح ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListExplanationsDto>> GetAllListExplanation()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<PriceListExplanationsDto>();
            }
        }
        /// <summary>
        /// گرفتن تمامی شرح ها بر مبنای فصلشون
        /// </summary>
        /// <param name="cluaseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListExplanationsDto>> GetAllListExplanationByClauseID(Guid cluaseId)
        {
            try
            {
                var result =await  _unitOfWork.PriceListExplanationRepository.GetPriceListExplanationByClauseID(cluaseId);
                var model = PriceListExplanationAutoMapperProfile.EntitiesToDtos(result, _errorLoggerService);
                return model;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceListExplanationsDto>();
            }
        }

        public async  Task<PriceListExplanationsDto> GetistExplanatio(Guid explanaionId)
        {
            try
            {
                return PriceListExplanationAutoMapperProfile.EntityToDto(await _unitOfWork.PriceListExplanationRepository.Get(explanaionId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                return new PriceListExplanationsDto();
            }
        }

        /// <summary>
        /// بروزرسانی یک شرح
        /// </summary>
        /// <param name="Explanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> UpdateExplanation(PriceListExplanationsDto Explanation)
        {
            try
            {
                return await _unitOfWork.PriceListExplanationRepository.Update(PriceListExplanationAutoMapperProfile.DtoToEntity(Explanation, _errorLoggerService));
            }
            catch (Exception ex)
            {

                return ("خطا در ویرایش شرح", false);
            }
        }
    }
}
