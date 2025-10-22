using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.Calculators.InvoiceInformationsCalculators;
using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class EstimatedMeterFinancialService : IEstimatedMeterFinancialService
    {
        private IUnitOfWork _unitOfWork;
        private IEstimatedMeterFinancialService _estimatedMeterFinancialService;
        private IErrorLoggerService _errorLoggerService;
        public EstimatedMeterFinancialService(IUnitOfWork unitOfWork, IEstimatedMeterFinancialService estimatedMeterFinancialService, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _estimatedMeterFinancialService = estimatedMeterFinancialService;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<GetAllEMFDto>> GetAllEMF(GetAllEMFIdsDto getAllEMFIdsDto)
        {
            try
            {
                var result = new List<GetAllEMFDto>();
                List<ContractEstimatedmeter> allContractEstimatedMeters = new List<ContractEstimatedmeter>();
                var allEstimatedMeterFinancials = await _unitOfWork.EstimatedMeterFinancialRepository.GetAllByInvoiceBaseInformationId(getAllEMFIdsDto.InvoiceBaseInformationId);
                var allServiceExplanations = await _unitOfWork.ServiceExplanationRepository.GetAll(getAllEMFIdsDto.ContractId);
                foreach (var sE in allServiceExplanations)
                {
                    if (sE.ExplanationType == ExplanationTypeEnum.PriceListExplanation)
                    {
                        allContractEstimatedMeters.AddRange(await _unitOfWork.ContractEstimatedmeterRepository.GetAll(sE.ID));
                    }
                }
                result = EstimatedMeterFinancialMapper.EntitiesToGetAllCFSDtos(allContractEstimatedMeters, allEstimatedMeterFinancials, _errorLoggerService);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<GetAllEMFDto>();
            }
        }
        public async Task<(string message, bool isSuccess)> CUDEMRequestedFinancial(CUDEMRequestedFinancialDto cUDEMRequestedFinancialDto, LoginUserDto userDto)
        {
            try
            {
                if (cUDEMRequestedFinancialDto.IsDelete)
                {
                    //Delete
                    await _unitOfWork.EstimatedMeterFinancialRepository.Delete(cUDEMRequestedFinancialDto.Key);
                    return ("ردیف با موفقیت حذف شد.", true);
                }
                if (cUDEMRequestedFinancialDto.IsFinancial)
                {
                    //Update
                    var CalculatedRequests = EstimatedMeterFinancialCalculator.CalculateRequestedProperties(cUDEMRequestedFinancialDto);
                    var model = await _unitOfWork.EstimatedMeterFinancialRepository.Get(cUDEMRequestedFinancialDto.Key);
                    if (model != null)
                    {
                        model.RequestedVolume = CalculatedRequests.CalculatedRequestedVolume;
                        model.RequestedPercent = CalculatedRequests.CalculatedRequestedPercent;
                        model.RequestedPrice = CalculatedRequests.CalculatedRequestedPrice;
                    }
                    await _unitOfWork.Save();
                    return ("ردیف با موفقیت ویرایش شد.", true);
                }
                else
                {
                    //Add
                    var CalculatedRequests = EstimatedMeterFinancialCalculator.CalculateRequestedProperties(cUDEMRequestedFinancialDto);
                    await _unitOfWork.EstimatedMeterFinancialRepository.Add(EstimatedMeterFinancialMapper.DtoToEntity(cUDEMRequestedFinancialDto, CalculatedRequests));
                    return ("ردیف جدید با موفقیت ثبت شد.", true);
                }
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("خطا در انجام عملیات.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> SetEMApprovedFinancial(SetEMApprovedFinancialDto setEMApprovedFinancialDto, LoginUserDto userDto)
        {
            try
            {
                var entity = await _unitOfWork.EstimatedMeterFinancialRepository.Get(setEMApprovedFinancialDto.EstimatedMeterFinancialId);
                var calculatedApprovedProperties = EstimatedMeterFinancialCalculator.CalculateApprovedProperties(setEMApprovedFinancialDto);
                if (entity == null)
                {
                    entity.ApprovedVolume = calculatedApprovedProperties.CalculatedApprovedVolume;
                    entity.ApprovedPercent = calculatedApprovedProperties.CalculatedApprovedPercent;
                    entity.ApprovedPrice = calculatedApprovedProperties.CalculatedApprovedPrice;
                    await _unitOfWork.Save();
                    return ("ردیف با موفقیت ویرایش شد.", true);
                }
                else
                {
                    return ("ردیف مورد نظر پیدا نشد.", false);
                }

            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("خطا در انجام عملیات.", false);
            }
        }
    }
}