 using AppCore.Entities.ContractsInformation.Contratcs;
 using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Entities.SettingEntities.CorespondentLegals;
 using AppCore.Entities.SettingEntities.CorespondentReals;
 using AppCore.Entities.User;
 using AppCore.UnitOfWork;
using ApplicationService.Common;
 using ApplicationService.DtoModels.ContractDtos;
 using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.PaymentDtos;
using ApplicationService.DtoModels.UserDtos;
 using ApplicationService.DtoModels.WFEDto;
 using ApplicationService.Mapper.ContractInformationMapper;
 using ApplicationService.Mapper.InvoiceInformationsMappers;
 using ApplicationService.Services.ContractInformationService;
 using ApplicationService.Services.ExceptionHandlingService;
 using ApplicationService.Services.WFEService;
 using ApplicationService.ServicesContract.ContractInformation;
 using ApplicationService.ServicesContract.ExceptionHandling;
 using ApplicationService.ServicesContract.History;
 using ApplicationService.ServicesContract.InvoiceInformations;
 using ApplicationService.ServicesContract.Users;
 using ApplicationService.ServicesContract.WFEInvoice;
 using Azure.Core;
 using Microsoft.Extensions.Configuration;
 using Newtonsoft.Json;
 using System;
 using System.Collections.Generic;
 using System.Diagnostics.Contracts;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class InvoiceBaseInformationService : IInvoiceBaseInformationService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private IErrorLoggerService _errorLoggerService;
        // DI for NettingAmount
        private INettingProcessItemService _nettingProcessService;
        // DI for Payment PaymentAmount
        private IPaymentService _paymentService;
        private IWfeInvoiceService _wfInvoiceService;
        private IInvoiceAccessGroupFilterService _filterService;
        private IContractAddendumService _contractAddendumService;
        private IHistoryService _historyService;
        private IUserService _userService;
        // DI for ServiceExplanations of Invoice's Contract
        private IServiceExplanationService _serviceExplanationService;
        // DI contractService
        private IContractService _contractService;
        public InvoiceBaseInformationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IConfiguration configuration, IWfeInvoiceService wfeInvoiceService,
            IInvoiceAccessGroupFilterService filterService, IContractAddendumService contractAddendumService, IHistoryService historyService, IUserService userService
            ,INettingProcessItemService nettingProcessService, IPaymentService paymentService, IServiceExplanationService serviceExplanationService, IContractService contractService   )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _wfInvoiceService = wfeInvoiceService;
            _filterService = filterService;
            _contractAddendumService = contractAddendumService;
            _historyService = historyService;
            _userService = userService;
            _nettingProcessService = nettingProcessService;
            _paymentService = paymentService;
            _serviceExplanationService = serviceExplanationService;
            _contractService = contractService;
        }


        public async Task<(string message, bool isSuccess, Guid invoiceBaseInformationId)> Add(InvoiceBaseInformationInsertDto invoiceBaseInformation, LoginUserDto userDto, string connectionString)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(userDto.FullQualifyName, connectionString);
                userDto.ID = userId;
                var wfe = await _wfInvoiceService.GetFirstStage(userDto.FullQualifyName);
                var entity = InvoiceBaseInformationMapper.DtoToEntity(invoiceBaseInformation, userDto, wfe, _errorLoggerService);
                // بررسی کد صورت وضعیت
                var duplicate = _unitOfWork.InvoiceBaseInformationRepository?.GetByInvoiceNumber(entity.InvoiceCode);
                if (duplicate != null)
                    return ("کد صورت وضعیت تکراریست", false, Guid.Empty);
                var result = await _unitOfWork.InvoiceBaseInformationRepository.Add(entity);
                // send request to WFE/InsertDefaultApprovers
                await _wfInvoiceService.InsertDefaultApprovers(userDto.FullQualifyName, entity.Id);
                await _historyService.AddHistory(user.ID, "افزودن صورت وضعیت", entity.Id, user.FullName);

                return ("ثبت  صورت وضعیت با موفقیت انجام شد.", true, result.invoiceBaseInformationId);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ثبت صورت وضعیت با خطا مواجه شد.", false, Guid.Empty);
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid id, LoginUserDto userDto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                await _historyService.AddHistory(user.ID, "حذف صورت وضعیت", id, user.FullName);
                var result = await _unitOfWork.InvoiceBaseInformationRepository.Delete(id);
                await _unitOfWork.ServiceExplanationFinancialRepository.DeleteByInvoiceId(id);
                await _unitOfWork.NettingProcessItemRepository.DeleteByInvoiceId(id);
                await _unitOfWork.PaymentRepository.DeleteByInvoiceId(id);
                await _unitOfWork.InvoiceAmountRepository.DeleteApprovedAmount(id);
                return result;

            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("حذف صورت وضعیت با خطا مواجه شد.", false);
            }
        }
        public async Task<InvoiceBaseInformationFullGetDto> Get(Guid id)
        {
            try
            {
                var entityResult = await _unitOfWork.InvoiceBaseInformationRepository.Get(id);
                // from ContractId => ServiceExplanations => Sum(Amount);
                string lastContractAmount = await _contractService.GetLastContractAmount(entityResult.ContractId);
                CorespondentLegal LegalCorespondent = null;
                CorespondentReal RealCorespondent = null;

                if (entityResult.Contract.CorespondentRealOrLegal == false)
                {
                    //Legal حقوقی
                    LegalCorespondent = await _unitOfWork.CorespondentLegalRepository.Get(entityResult.Contract.CorespondentID);
                }
                else
                {
                    //Real حقیقی
                    RealCorespondent = await _unitOfWork.CorespondentRealRepository.Get(entityResult.Contract.CorespondentID);
                }
                // NettingProcessItemsService  برای نمایش مجموع مبالغ ناخالص تایید شده
                // مجموع ناخالص تاییده شده 
                List<ServiceExplanationFinancial> sefs = entityResult.ServiceExplanationFinancials;
                //decimal nettingProcessItemsAmount = sefs?.Sum(s => s.ApprovedPrice) ?? 0m;
                decimal nettingProcessItemsAmount = 0m;

                if (sefs != null)
                {
                    foreach (var item in sefs)
                    {
                        nettingProcessItemsAmount += item.ApprovedPrice;
                    }
                }

                //decimal nettingProcessItemsAmount = 0;
                // فقط مبالغ پرداخت شده به عنوان متغییر
                // مجموع مبالغ پرداخت شده
                List<NettedAmountGetDto> paymentAmountDtos = await _paymentService.GetAllBeforThisInvoiceBaseInformationId(id);
                decimal paymentAmount = paymentAmountDtos.Sum(x => x.NettedAmount);
                
                var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount(id);
                var model = InvoiceBaseInformationFullAutoMapper.EntityToGetDto(
                        entityResult,          
                        LegalCorespondent,      
                        RealCorespondent,      
                        invoiceAmounts,         
                        nettingProcessItemsAmount, 
                        paymentAmount,          
                        lastContractAmount,  
                        _errorLoggerService);
                return model;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new InvoiceBaseInformationFullGetDto();
            }
        }
        public async Task<List<InvoiceBaseInformationGetAllDto>> GetAll(string fullQualifyName, string connectionString)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var invoiceIds = await _wfInvoiceService.GetAllUserCanSeenIds(fullQualifyName);
                var filteredData = await _filterService.FilterInvoice(await _unitOfWork.InvoiceBaseInformationRepository.GetAll(), invoiceIds, userId);
                if (filteredData.Any())
                {
                    var LegalCorespondents = new List<CorespondentLegal>();
                    var RealCorespondents = new List<CorespondentReal>();
                    LegalCorespondents = await _unitOfWork.CorespondentLegalRepository.GetAll();
                    RealCorespondents = await _unitOfWork.CorespondentRealRepository.GetAll();
                    var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();
                    var results = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(filteredData, LegalCorespondents, RealCorespondents, invoiceAmounts, _errorLoggerService);
                    return results;
                }
                return new List<InvoiceBaseInformationGetAllDto>();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformationGetAllDto>();
            }
        }
        public async Task<List<InvoiceBaseInformationGetAllDto>> GetAllForContract(Guid contractId, string fullQualifyName, string connectionString)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var InvoiceForThisContract = await _unitOfWork.InvoiceBaseInformationRepository.GetAllByContractId(contractId);
                var filteredData = await _filterService.FilterInvoice(InvoiceForThisContract, new List<Guid>(), userId);
                if (filteredData.Any())
                {
                    var LegalCorespondents = new List<CorespondentLegal>();
                    var RealCorespondents = new List<CorespondentReal>();
                    LegalCorespondents = await _unitOfWork.CorespondentLegalRepository.GetAll();
                    RealCorespondents = await _unitOfWork.CorespondentRealRepository.GetAll();
                    var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();
                    var results = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(filteredData, LegalCorespondents, RealCorespondents, invoiceAmounts, _errorLoggerService);
                    return results;
                }
                return new List<InvoiceBaseInformationGetAllDto>();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformationGetAllDto>();
            }
        }
        public async Task<List<InvoiceBaseInformationGetAllDto>> GetAllMine(string fullQualifyName, string connectionString)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var entities = await _unitOfWork.InvoiceBaseInformationRepository.GetAllMine(userId);
                var LegalCorespondents = new List<CorespondentLegal>();
                var RealCorespondents = new List<CorespondentReal>();
                LegalCorespondents = await _unitOfWork.CorespondentLegalRepository.GetAll();
                RealCorespondents = await _unitOfWork.CorespondentRealRepository.GetAll();
                var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();
                var results = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(entities, LegalCorespondents, RealCorespondents, invoiceAmounts, _errorLoggerService);
                return results;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformationGetAllDto>();
            }
        }
        public async Task<List<InvoiceBaseInformationGetAllDto>> GetAllWaitingForAction(string fullQualifyName, string connectionString)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var invoiceIds = await _wfInvoiceService.InvoiceGetEntitiesAwaitingUserAction(fullQualifyName);
                var entities = await _unitOfWork.InvoiceBaseInformationRepository.GetAllWaitingForAction(invoiceIds);
                var LegalCorespondents = new List<CorespondentLegal>();
                var RealCorespondents = new List<CorespondentReal>();
                LegalCorespondents = await _unitOfWork.CorespondentLegalRepository.GetAll();
                RealCorespondents = await _unitOfWork.CorespondentRealRepository.GetAll();
                var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();
                var results = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(entities, LegalCorespondents, RealCorespondents, invoiceAmounts, _errorLoggerService);
                return results;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformationGetAllDto>();
            }
        }


        //GetAllWaitingForAprove Added by Bahman
        public async Task<List<InvoiceBaseInformationGetAllDto>> GetAllWaitingForApprove(string fullQualifyName, string connectionString)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var invoiceIds = await _wfInvoiceService.InvoiceGetEntitiesAwaitingApproval(fullQualifyName);
                var entities = await _unitOfWork.InvoiceBaseInformationRepository.GetAllWaitingForAction(invoiceIds);
                var LegalCorespondents = new List<CorespondentLegal>();
                var RealCorespondents = new List<CorespondentReal>();
                LegalCorespondents = await _unitOfWork.CorespondentLegalRepository.GetAll();
                RealCorespondents = await _unitOfWork.CorespondentRealRepository.GetAll();
                var invoiceAmounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();
                var results = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(entities, LegalCorespondents, RealCorespondents, invoiceAmounts, _errorLoggerService);
                return results;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<InvoiceBaseInformationGetAllDto>();
            }
        }
        public async Task<GetContractCorespondentInformationsDto> GetContractCorespondentInformations(Guid contractId)
        {
            try
            {
                AppCore.Entities.ContractsInformation.Contratcs.Contract relatedContract = await _unitOfWork.ContractRepository.Get(contractId);
                CorespondentLegal LegalCorespondent = null;
                CorespondentReal RealCorespondent = null;
                if (relatedContract.CorespondentRealOrLegal == false)
                {
                    //Legal حقوقی
                    LegalCorespondent = await _unitOfWork.CorespondentLegalRepository.Get(relatedContract.CorespondentID);
                }
                else
                {
                    //Real حقیقی
                    RealCorespondent = await _unitOfWork.CorespondentRealRepository.Get(relatedContract.CorespondentID);
                }
                var model = InvoiceBaseInformationMapper.CorespondentEntityToDto(LegalCorespondent, RealCorespondent, _errorLoggerService);
                return model;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new GetContractCorespondentInformationsDto();
            }
        }


        public async Task<(string message, bool isSuccess)> Update(InvoiceBaseInformationInsertDto invoicebaseinformation, LoginUserDto userdto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userdto.FullQualifyName, _configuration["connectionstrings:dbconnection"]);
                var entity = InvoiceBaseInformationMapper.DtoToEntity(invoicebaseinformation, userdto, null, _errorLoggerService);
                var contractupdate = await _unitOfWork.InvoiceBaseInformationRepository.Update(entity);
                await _historyService.AddHistory(user.ID, "ویرایش صورت وضعیت", entity.Id, user.FullName);
                return ("صورت وضعیت با موفقیت بروزرسانی شد", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("بروز رسانی  صورت وضعیت با خطا مواجه شد.", false);
            }
        }


        public async Task<List<InvoiceBaseInformationGetAllDto>> SearchAll(string fullQualifyName, string connectionString, string? term)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);

                var entities = await _unitOfWork.InvoiceBaseInformationRepository.SearchAllAsync(term);

                // اگر دسترسی/گروه را in-memory اعمال می‌کنی، مثل قبل:
                var canSeeIds = await _wfInvoiceService.GetAllUserCanSeenIds(fullQualifyName);
                var filtered = await _filterService.FilterInvoice(entities, canSeeIds, userId);

                if (filtered?.Any() == true)
                {
                    var legals = await _unitOfWork.CorespondentLegalRepository.GetAll();
                    var reals = await _unitOfWork.CorespondentRealRepository.GetAll();
                    var amounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();

                    var dtos = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(filtered, legals, reals, amounts, _errorLoggerService);
                    return dtos.AsQueryable().OrderDefault().ToList(); // SendDate DESC, InvoiceNumber
                }
                return new();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new();
            }
        }

        public async Task<List<InvoiceBaseInformationGetAllDto>> SearchMine(string fullQualifyName, string connectionString, string? term)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);

                var entities = await _unitOfWork.InvoiceBaseInformationRepository.SearchMineAsync(userId, term);

                var legals = await _unitOfWork.CorespondentLegalRepository.GetAll();
                var reals = await _unitOfWork.CorespondentRealRepository.GetAll();
                var amounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();

                var dtos = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(entities, legals, reals, amounts, _errorLoggerService);
                return dtos.AsQueryable().OrderDefault().ToList();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new();
            }
        }

        public async Task<List<InvoiceBaseInformationGetAllDto>> SearchWaitForAction(string fullQualifyName, string connectionString, string? term)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);
                var ids = await _wfInvoiceService.GetAllWaitingForActionIds(fullQualifyName);

                var entities = await _unitOfWork.InvoiceBaseInformationRepository.SearchWaitForActionAsync(ids, term);

                var legals = await _unitOfWork.CorespondentLegalRepository.GetAll();
                var reals = await _unitOfWork.CorespondentRealRepository.GetAll();
                var amounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();

                var dtos = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(entities, legals, reals, amounts, _errorLoggerService);
                return dtos.AsQueryable().OrderDefault().ToList();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new();
            }
        }

        public async Task<List<InvoiceBaseInformationGetAllDto>> SearchByContract(Guid contractId, string fullQualifyName, string connectionString, string? term)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetLoggedInUserId(fullQualifyName, connectionString);

                var entities = await _unitOfWork.InvoiceBaseInformationRepository.SearchByContractAsync(contractId, term);

                // اگر سیاستت اعمال Access برای همین قرارداد است:
                var filtered = await _filterService.FilterInvoice(entities, new List<Guid>(), userId);
                if (filtered == null)
                    filtered = new List<InvoiceBaseInformation>();

                var legals = await _unitOfWork.CorespondentLegalRepository.GetAll();
                var reals = await _unitOfWork.CorespondentRealRepository.GetAll();
                var amounts = await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount();

                var dtos = InvoiceBaseInformationMapper.EntitiesToGetAllDtos(filtered, legals, reals, amounts, _errorLoggerService);
                return dtos.AsQueryable().OrderDefault().ToList();
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new();
            }
        }
    }
}

