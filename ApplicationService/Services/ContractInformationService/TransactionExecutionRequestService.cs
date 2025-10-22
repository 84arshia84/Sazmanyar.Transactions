using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.Services.WFEContractService;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.PublicEntities;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    public class TransactionExecutionRequestService : ITransactionExecutionRequestService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IDescriptionService _descriptionService;
        private IAttachService _attachService;
        private IWfeTransactionExecutionRequestService _wfeTranactionService;
        private IUserService _userService;
        private IConfiguration _configuration;
        private IContractAccessGroupFilterService _filterService;
        private IExecutionRequestServiceExplanationService _serviceExplanationService;
        private IExecutionRequestCheckListValueService _checkListValueService;
        private IHistoryService _historyService;
        //
        //private IWfeContractService _wfeContractService;
       // private IContractTimeProfileService _contractTimeProfileService;
       // private IContractFinancialDetailsService _contractFinancialDetailsService;
       // private IContractService _contractService;
       // private IContractGuaranteeService _contractGuaranteeService;
        public TransactionExecutionRequestService(
            IUnitOfWork unitOfWork,
            IErrorLoggerService errorLoggerService,
            IDescriptionService descriptionService,
            IAttachService attachService,
            IWfeTransactionExecutionRequestService wfeTranactionService,
            IUserService userService,
            IConfiguration configuration,
            IContractAccessGroupFilterService filterService,
            IExecutionRequestServiceExplanationService serviceExplanationService,
            IExecutionRequestCheckListValueService checkListValueService,
            IHistoryService historyService

            )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _descriptionService = descriptionService;
            _attachService = attachService;
            _filterService = filterService;
            _userService = userService;
            _configuration = configuration;
            _filterService = filterService;
            _wfeTranactionService = wfeTranactionService;
            _serviceExplanationService = serviceExplanationService;
            _checkListValueService = checkListValueService;
            _historyService = historyService; 

            
        }
        public async Task<(string message, bool isSuccess)> Add(TransactionExecutionRequestDto transactionDto, LoginUserDto userDto)
        {
            var publicEntityId = Guid.Empty;
            try
            {

                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var wfe = await _wfeTranactionService.GetFirstStage(userDto.FullQualifyName);
                var entity = await TransactionExecutionRequestAutoMapperProfile.DtoToEntity(transactionDto, user, _errorLoggerService);
                if (entity.serviceExplanation == null)
                {
                    return ("درخواست بدون شرح خدمت  ثبت نمی شود.", false);
                }
                if (wfe != null)
                {
                    entity.transaction.CurrentStageId = wfe.Id;
                }
                publicEntityId = entity.transaction.Id;
                var duplicate = await _unitOfWork.TransactionExecutionRequestRepository.GetByRequestNumber(entity.transaction.NumberOfRequest);
                if (duplicate != null)
                    return ("کد درخواست تکراری است", false);
                var insertContract = await _unitOfWork.TransactionExecutionRequestRepository.Add(entity.transaction);
                if (insertContract.isSuccess)
                {
                    await _unitOfWork.Save();
                    await _wfeTranactionService.InsertDefaultApprovers(user.FullQualifyName, entity.transaction.Id);
                    await _serviceExplanationService.AddServiceExplanation(entity.serviceExplanation);
                    await _unitOfWork.Save();
                    if (transactionDto.descriptionDtos != null)
                    {
                        await _descriptionService.Add(transactionDto.descriptionDtos, false, entity.transaction.Id);
                    }
                    if (transactionDto.attachDtos != null)
                    {
                        await _attachService.Add(transactionDto.attachDtos, false, entity.transaction.Id);
                    }
                    await _historyService.AddHistory(user.ID, "افزودن درخواست برگزاری معامله", entity.transaction.Id, user.FullName);
                    await _unitOfWork.Save();
                    return ("درخواست برگزاری با موفقیت ثبت شد", true);
                }
                return ("ثبت درخواست برگزاری با خطا مواجه شد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                await DeleteExceptinalTransAction(publicEntityId);
                return ("خطا در اتصال به دیتابیس", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id, string userDto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto, _configuration["ConnectionStrings:DbConnection"]);
                await _historyService.AddHistory(user.ID, "حذف درخواست برگزاری معامله", id, user.FullName);
                return await _unitOfWork.TransactionExecutionRequestRepository.Delete(id, user.ID);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<TransactionExecutionRequestDto> Get(Guid id)
        {
            try
            {
                var model = await TransactionExecutionRequestAutoMapperProfile.EntityToDto(await _unitOfWork.TransactionExecutionRequestRepository.Get(id), _errorLoggerService);
                return model;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new TransactionExecutionRequestDto();
            }
        }

        public async Task<List<TransactionExecutionRequestDto>> GetAll(string userName, int situation)
        {
            try
            {

                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.Mine)
                {
                    return await TransactionExecutionRequestAutoMapperProfile.EntitesToDtos(await _unitOfWork.TransactionExecutionRequestRepository.GetAllMine(user.ID), _errorLoggerService);
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForAction)
                {
                    var transactionIds = await _wfeTranactionService.GetEntitiesAwaitingUserAction(userName);
                    if (transactionIds?.Any() == true)
                    {
                        return await TransactionExecutionRequestAutoMapperProfile.EntitesToDtos(await _unitOfWork.TransactionExecutionRequestRepository.GetAllWaitForAction(transactionIds), _errorLoggerService);
                    }
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForConfirm)
                {
                    var transactionIds = await _wfeTranactionService.GetEntitiesAwaitingApproval(userName);
                    if (transactionIds?.Any() == true)
                    {
                        return await TransactionExecutionRequestAutoMapperProfile.EntitesToDtos(await _unitOfWork.TransactionExecutionRequestRepository.GetAllWaitForAction(transactionIds), _errorLoggerService);
                    }
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.All)
                {
                    var transactionIds = await _wfeTranactionService.GetAllUserCanSeenIds(userName);
                    var filteredData = await _filterService.FilterTransActionExecutionRequest(await _unitOfWork.TransactionExecutionRequestRepository.GetAll(), transactionIds, user.ID);
                    if (filteredData != null)
                    {
                        return await TransactionExecutionRequestAutoMapperProfile.EntitesToDtos(filteredData, _errorLoggerService);
                    }
                }
                return new List<TransactionExecutionRequestDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionExecutionRequestDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(TransactionExecutionRequestDto transactionDto, LoginUserDto userDto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var entityBeforUpdate = await _unitOfWork.TransactionExecutionRequestRepository.Get(transactionDto.id);
                var entity = await TransactionExecutionRequestAutoMapperProfile.DtoToEntity(transactionDto, userDto, _errorLoggerService);

                entity.transaction.InsertBy = entityBeforUpdate.InsertBy;
                entity.transaction.InsertDate = entityBeforUpdate.InsertDate;
                entity.transaction.IsFinalApprove = entityBeforUpdate.IsFinalApprove;
                entity.transaction.LastActionTitle = entityBeforUpdate.LastActionTitle;
                entity.transaction.CurrentStatusTitle = entityBeforUpdate.CurrentStatusTitle;
                entity.transaction.CurrentStageId = entityBeforUpdate.CurrentStageId;
                if (entity.serviceExplanation == null)
                {
                    return ("درخواست بدون شرح خدمت  ثبت شد.", false);
                }
                var transactionUpdate = await _unitOfWork.TransactionExecutionRequestRepository.Update(entity.transaction);
                if (transactionUpdate.isSuccess == true)
                {
                    if (entity.transaction.ExecutionRequestCheckListValue == null)
                    {
                        await _checkListValueService.DeleteCheckListValues(entity.transaction.Id);
                    }
                    if (entity.transaction.ExecutionRequestCheckListValue != null)
                    {
                        await _checkListValueService.UpdateCheckListValues(entity.transaction.ExecutionRequestCheckListValue);
                    }
                    if (transactionDto.descriptionDtos != null)
                    {
                        await _descriptionService.Update(transactionDto.descriptionDtos, false, entity.transaction.Id);
                    }
                    if (transactionDto.attachDtos != null)
                    {
                        await _attachService.Update(transactionDto.attachDtos, false, entity.transaction.Id);
                    }
                    await _historyService.AddHistory(user.ID, "ویرایش قرارداد", entity.transaction.Id, user.FullName);
                    await _unitOfWork.Save();
                    var serviceExplanation = await _serviceExplanationService.UpdateServiceExplanation(entity.serviceExplanation);
                    await _unitOfWork.Save();
                    return ("درخواست با موفقیت بروزرسانی شد", true);
                }
                return ("بروز رسانی  درخواست با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }

        }
        internal async Task DeleteExceptinalTransAction(Guid transactionId)
        {
            try
            {
                await _unitOfWork.TransactionExecutionRequestRepository.DeleteExceptionalTransaction(transactionId);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public async Task<List<TransactionExecutionRequestSubjectAndIdDto>> GetAllApproved(string userName)
        {
            try
            {
                var flowIds = new List<Guid>();

                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var transactions = await _unitOfWork.TransactionExecutionRequestRepository.GetAllFinalApproved();
                var filteredTrans = await _filterService.FilterTransActionExecutionRequest(transactions, flowIds ,user.ID);
                var dtos = TransactionExecutionRequestSubjectAndIdMapper.EntitiesToDtos(filteredTrans, _errorLoggerService);

                return dtos;
                //return new List<TransactionExecutionRequestDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<TransactionExecutionRequestSubjectAndIdDto>();
            }
        }



    }

    }






    