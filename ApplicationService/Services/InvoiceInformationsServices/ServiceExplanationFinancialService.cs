using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.Pwa;
using ApplicationService.ServicesContract.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class ServiceExplanationFinancialService : IServiceExplanationFinancialService
    {
        private IUnitOfWork _unitOfWork;
        private IInvoiceBaseInformationService _invoiceBaseInformationService;
        private IServiceExplanationService _serviceExplanationService;
        private INettingProcessItemService _nettingProcessItemService;
        private IErrorLoggerService _errorLoggerService;
        private IPrePaymentDepreciationService _paymentDepreciationService;
        private IAccountService _onAccountDepreciationService;
        private IContractAddendumService _contractAddendumService;
        private IUserService _userService;
        private IConfiguration _configuration;
        private IProjectService _projectService;
        public ServiceExplanationFinancialService(IUnitOfWork unitOfWork, IServiceExplanationService serviceExplanationService, INettingProcessItemService nettingProcessItemService,
            IInvoiceBaseInformationService invoiceBaseInformationService, IErrorLoggerService errorLoggerService, IPrePaymentDepreciationService paymentDepreciationService,
            IAccountService onAccountDepreciationService, IContractAddendumService contractAddendumService,IProjectService projectService,
            IUserService userService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _invoiceBaseInformationService = invoiceBaseInformationService;
            _serviceExplanationService = serviceExplanationService;
            _nettingProcessItemService = nettingProcessItemService;
            _errorLoggerService = errorLoggerService;
            _paymentDepreciationService = paymentDepreciationService;
            _onAccountDepreciationService = onAccountDepreciationService;
            _contractAddendumService = contractAddendumService;
            _userService = userService;
            _configuration = configuration;
            _projectService = projectService;
        }
        public async Task<(string message, bool isSuccess)> SetSEApprovedFinancial(SetSEApprovedFinancialDto setSEApprovedFinancialDto, LoginUserDto userDto)
        {
            try
            {
                var entity = await _unitOfWork.ServiceExplanationFinancialRepository.Get(setSEApprovedFinancialDto.ServiceExplanationFinancailId);
                var calculatedApprovedProperties = ServiceExplanationFinancilaCalculationService.CalculateApprovedProperties(setSEApprovedFinancialDto);
                if (entity != null)
                {
                    entity.ApprovedVolume = calculatedApprovedProperties.CalculatedApprovedVolume;
                    entity.ApprovedPercent = calculatedApprovedProperties.CalculatedApprovedPercent;
                    entity.ApprovedPrice = calculatedApprovedProperties.CalculatedApprovedPrice;
                    await _unitOfWork.Save();
                    await _paymentDepreciationService.CalculatingSugestedPrepayment(setSEApprovedFinancialDto.ServiceExplanationFinancailId);
                    await _onAccountDepreciationService.CalculatingSugestedOnAccount(setSEApprovedFinancialDto.ServiceExplanationFinancailId);
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
        public async Task<(string message, bool isSuccess)> CUDSERequestedFinancial(CUDSERequestedFinancialDto cUDSERequestedFinancialDto, LoginUserDto userDto)
        {
            try
            {
                if (cUDSERequestedFinancialDto.IsDelete)
                {
                    return await DeleteServiceExplanationFinancial(cUDSERequestedFinancialDto.Key);
                }
                if (cUDSERequestedFinancialDto.IsFinancial)
                {
                    return await UpdateServiceExplanationFinancial(cUDSERequestedFinancialDto);
                }
                else
                {
                    return await AddServiceExplanationFinancial(cUDSERequestedFinancialDto, userDto);
                }
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("خطا در انجام عملیات.", false);
            }
        }
        internal async Task<(string message, bool isSuccess)> DeleteServiceExplanationFinancial(Guid id)
        {
            try
            {
                await _unitOfWork.ServiceExplanationFinancialRepository.Delete(id);
                return ("ردیف با موفقیت حذف شد.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف", false);
            }
        }
        internal async Task<(string message, bool isSuccess)> UpdateServiceExplanationFinancial(CUDSERequestedFinancialDto cUDSERequestedFinancialDto)
        {
            try
            {
                var CalculatedRequests = ServiceExplanationFinancilaCalculationService.CalculateRequestedProperties(cUDSERequestedFinancialDto);
                if (!await ServiceExplanationValidationService.CheckPrePaymentPercent(cUDSERequestedFinancialDto.InvoiceBaseInformationId, cUDSERequestedFinancialDto.ServiceExplanationId, CalculatedRequests.CalculatedRequestedPercent
                    , _unitOfWork, _errorLoggerService))
                    return ("مقدار وارد شده بیش از مقدار پیش پرداخت هست", false);
                if (!await ServiceExplanationValidationService.CheckMaxPercentThatFinancialHasAccess(cUDSERequestedFinancialDto.InvoiceBaseInformationId, cUDSERequestedFinancialDto.ServiceExplanationId, CalculatedRequests.CalculatedRequestedPercent
                    , _unitOfWork, _errorLoggerService))
                    return ("مقداری که وارد کردین از جمع مقدار وارد شده بیشتر می باشد", false);
                await _unitOfWork.ServiceExplanationFinancialRepository.Update(ServiceExplanationFinancialMapper.DtoToEntity(cUDSERequestedFinancialDto, CalculatedRequests));
                await _unitOfWork.Save();
                return ("ردیف با موفقیت ویرایش شد.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروز رسانی", false);
            }
        }
        internal async Task<(string message, bool isSuccess)> AddServiceExplanationFinancial(CUDSERequestedFinancialDto cUDSERequestedFinancialDto, LoginUserDto userDto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var CalculatedRequests = ServiceExplanationFinancilaCalculationService.CalculateRequestedProperties(cUDSERequestedFinancialDto);
                if (!await ServiceExplanationValidationService.CheckPrePaymentPercent(cUDSERequestedFinancialDto.InvoiceBaseInformationId, cUDSERequestedFinancialDto.ServiceExplanationId, CalculatedRequests.CalculatedRequestedPercent, _unitOfWork, _errorLoggerService))
                    return ("مقدار وارد شده بیش از مقدار پیش پرداخت هست", false);
                if (!await ServiceExplanationValidationService.CheckMaxPercentThatFinancialHasAccess(cUDSERequestedFinancialDto.InvoiceBaseInformationId, cUDSERequestedFinancialDto.ServiceExplanationId, CalculatedRequests.CalculatedRequestedPercent, _unitOfWork, _errorLoggerService))
                    return ("مقداری که وارد کردین از جمع مقدار وارد شده بیشتر می باشد", false);

                //در این قسمت چک میکنیم ببینیم دیتاهای دیفالت خالص سازی قبلا اضافه شدن یا نه. اگه نشدن اضافه می کنیم
                var serviceExplanation = await _serviceExplanationService.GetById(cUDSERequestedFinancialDto.ServiceExplanationId);
                await _nettingProcessItemService.AddDefaultNettingProcess(cUDSERequestedFinancialDto, serviceExplanation, user);
                cUDSERequestedFinancialDto.CurrencyId = serviceExplanation.CurrencyID;
                await _unitOfWork.ServiceExplanationFinancialRepository.Add(ServiceExplanationFinancialMapper.DtoToEntity(cUDSERequestedFinancialDto, CalculatedRequests));
                return ("ردیف جدید با موفقیت ثبت شد.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ثبت شرح خدمت", false);

            }
        }
        /// <summary>
        /// mode
        /// برای زمانی است که ببینیم صورت وضعیت در دست چه نقشی هست 
        /// اگر از دست پیمانکار خارج بشود فقط 
        /// ServiceExplanationFinancial
        /// را باید ببیند
        /// mode = true دست پیمانکار
        /// </summary>
        /// <param name="getAllCFSIdsDto"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<List<GetAllCSFDto>> GetAllCFS(GetAllCSFIdsDto getAllCFSIdsDto, bool mode)
        {
            try
            {
                var result = new List<GetAllCSFDto>();
                var allAddendumServiceExplenations = new List<ServiceExplanation>();
                var Projects = await _projectService.GetAll();
                var allServiceExplanationFinancials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(getAllCFSIdsDto.InvoiceBaseInformationId);
                var addendumIds = await _contractAddendumService.GetLastAddendumOfContract(getAllCFSIdsDto.ContractId);
                if (addendumIds != null && addendumIds != Guid.Empty)
                {
                    allAddendumServiceExplenations = await _serviceExplanationService.GetAllServiceExplenationOfAddendumForInvoice(getAllCFSIdsDto.ContractId, addendumIds);
                    if (allAddendumServiceExplenations.Count > 0)
                    {
                        result = ServiceExplanationFinancialMapper.EntitiesToGetAllCFSDtos(allAddendumServiceExplenations, allServiceExplanationFinancials, mode, Projects, _errorLoggerService);
                        return result;
                    }
                }
                var allContractServiceExplanations = await _unitOfWork.ServiceExplanationRepository.GetAll(getAllCFSIdsDto.ContractId);
                result = ServiceExplanationFinancialMapper.EntitiesToGetAllCFSDtos(allContractServiceExplanations, allServiceExplanationFinancials, mode, Projects, _errorLoggerService);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<GetAllCSFDto>();
            }
        }
    }
}
