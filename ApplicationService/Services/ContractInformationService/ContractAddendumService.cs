using AppCore.Entities.ContractsInformation.ContractAddendums;
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
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractAddendumService : IContractAddendumService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IContractService _contractService;
        private IWfeContractAddendumService _wfeContractAddendumService;
        private IUserService _userService;
        private IConfiguration _configuration;
        private IContractAccessGroupFilterService _filterService;
        private IHistoryService _historyService;
        private IServiceExplanationService _explanationService;
        public ContractAddendumService(
            IUnitOfWork unitOfWork,
            IErrorLoggerService errorLoggerService,
            IContractService contractService,
            IWfeContractAddendumService wfeContractAddendumService,
            IUserService userService,
            IConfiguration configuration,
            IContractAccessGroupFilterService filterService,
            IHistoryService historyService,
            IServiceExplanationService serviceExplanation
            )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _contractService = contractService;
            _wfeContractAddendumService = wfeContractAddendumService;
            _userService = userService;
            _configuration = configuration;
            _filterService = filterService;
            _historyService = historyService;
            _explanationService = serviceExplanation;
        }
        public async Task<(string message, bool isSuccess)> Add(ContractAddendumDto contractAddendum, string username)
        {
            var publicAddendumId = Guid.Empty;
            try
            {
                var user = await _userService.GetByFullQualifyName(username, _configuration["ConnectionStrings:DbConnection"]);
                var wfe = await _wfeContractAddendumService.GetFirstStage(user.FullQualifyName);
                var lastAddendumId = await GetLastAddendumOfContract(contractAddendum.ContractId);
                contractAddendum.Id = Guid.NewGuid();
                publicAddendumId = contractAddendum.Id;
                var model = await ContractAddendumAutoMapperProfile.DtoToEntity(contractAddendum, user, _errorLoggerService);
                model.InsertAddendumBy = user.ID;
                model.InsertAddendumDate = DateTime.Now;
                if (wfe != null)
                {
                    model.CurrentStageId = wfe.Id;
                    model.CurrentStatusTitle = $"در انتظار ارسال کاربر {user.FullQualifyName}";
                    model.LastActionTitle = $"ثبت شده توسط کاربر {user.FullQualifyName}";
                }
                var result = await _unitOfWork.ContractAddendumRepository.Add(model);
                await _unitOfWork.Save();
                await _wfeContractAddendumService.InsertDefaultApprovers(user.FullQualifyName, model.Id);
                result = _contractService.UpdateInAddendum(contractAddendum.Contract, user, contractAddendum.Id).Result.isSuccess;
                await _historyService.AddHistory(user.ID, "افزودن الحاقیه در قرارداد", model.Id, user.FullName);
                await _unitOfWork.Save();
                if (result)
                {
                    
                    var value = await ContractAddendumChangePriceService.CalculateAddendumAltersInAdd(contractAddendum.ContractId, publicAddendumId, lastAddendumId, _explanationService, _unitOfWork,_errorLoggerService);
                    await _unitOfWork.ContractAddendumRepository.UpdateAddendumChangedValue(publicAddendumId,value);
                    await _unitOfWork.Save();

                    return ("ثبت الحاقیه موفقیت آمیز بود.", true);
                }
                return ("ثبت الحاقیه با خطا روبرو شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                await DeleteExceptinalContractAddendum(publicAddendumId);
                return ("ثبت الحاقیه با خطا روبرو شد.", false);
            }
        }

        public Task<(string message, bool isSuccess)> Add(List<ContractAddendumDto> contractAddendum)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid addendumId, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                await _historyService.AddHistory(user.ID, "حذف الحاقیه در قرارداد", addendumId, user.FullName);
                var Addendum = await _unitOfWork.ContractAddendumRepository.Get(addendumId);
                var result = await _unitOfWork.ContractAddendumRepository.Delete(addendumId, user.ID);
                if (result)
                {
                    await _unitOfWork.Save();
                    await CheckDoesContractHaveAnyAddendum(Addendum.ContractId);
                    await _explanationService.DeleteAllServiceExplanationForThisAddendum(addendumId);
                    return ("حذف الحاقیه موفقیت آمیز بود", true);
                }
                return ("الحاقیه جهت حذف یافت نشد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("حذف الحاقیه با خطا مواجه شد", false);
            }
        }

        public Task<ContractAddendumDto> Get(Guid addendumId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// آخرین الحاقیه ای که برای یک قرارداد ثبت شده است
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<Guid> GetLastAddendumOfContract(Guid contractId)
        {
            try
            {
                return await _unitOfWork.ContractAddendumRepository.GetLastAddendumOfContract(contractId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return Guid.Empty;
            }
        }
        public async Task<List<ContractAddendumDto>> GetAll(string username, int situation)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(username, _configuration["ConnectionStrings:DbConnection"]);
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.Mine)
                {
                    return await ContractAddendumAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ContractAddendumRepository.GetAllMine(user.ID), _errorLoggerService);
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForAction)
                {
                    var AddendumIds = await _wfeContractAddendumService.GetEntitiesAwaitingUserAction(username);
                    if (AddendumIds?.Any() == true)
                    {
                        return await ContractAddendumAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ContractAddendumRepository.GetAllWaitForAction(AddendumIds), _errorLoggerService);
                    }
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForConfirm)
                {
                    var AddendumIds = await _wfeContractAddendumService.GetEntitiesAwaitingApproval(username);
                    if (AddendumIds?.Any() == true)
                    {
                        return await ContractAddendumAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ContractAddendumRepository.GetAllWaitForAction(AddendumIds), _errorLoggerService);
                    }
                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.All)
                {
                    var AddendumIds = await _wfeContractAddendumService.GetAllUserCanSeenIds(username);
                    var filteredData = await _filterService.FilterContractAddendums(await _unitOfWork.ContractAddendumRepository.GetAll(), AddendumIds, user.ID);
                    if (filteredData?.Any() == true)
                    {
                        return await ContractAddendumAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                    }
                }
                return new List<ContractAddendumDto>();
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<ContractAddendumDto>();
            }
        }

        public async Task<List<ContractAddendumDto>> GetAll(Guid contractId)
        {
            try
            {
                return await ContractAddendumAutoMapperProfile.EntitiesToDtos(
                      await _unitOfWork.ContractAddendumRepository.GetAll(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractAddendumDto>();
            }
        }
        /// <summary>
        /// این تابع برای زمانی هست که کاربر در قسمت قرارداد ها درساید بار سمت چپ
        /// روی دکمه الحاقیه بزند و بخواهد الحاقیه مربوط به اون قرارداد را براساس تاثیر گروه دسترسی 
        /// مشاده کند
        /// </summary>
        /// <param name="username"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractAddendumDto>> GetAll(string username, Guid contractId)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(username, _configuration["ConnectionStrings:DbConnection"]);
                var addendums = await _unitOfWork.ContractAddendumRepository.GetAll(contractId);
                var filteredData = await _filterService.FilterContractAddendums(addendums, new List<Guid>(), user.ID);
                if (filteredData?.Any() == true)
                {
                    return await ContractAddendumAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                }
                return new List<ContractAddendumDto>();
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<ContractAddendumDto>();
            }
        }
        public async Task<(string message, bool isSuccess)> Update(ContractAddendumDto contractAddendum, LoginUserDto user)
        {
            try
            {
                var useer = await _userService.GetByFullQualifyName(user.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var beforUpdateModel = await _unitOfWork.ContractAddendumRepository.Get(contractAddendum.Id);
                var model = await ContractAddendumAutoMapperProfile.DtoToEntity(contractAddendum, user, _errorLoggerService);
                model.InsertAddendumBy = beforUpdateModel.InsertAddendumBy;
                model.InsertAddendumDate = beforUpdateModel.InsertAddendumDate;
                model.CurrentStageId = beforUpdateModel.CurrentStageId;
                model.CurrentStatusTitle = beforUpdateModel.CurrentStatusTitle;
                model.LastActionTitle = beforUpdateModel.LastActionTitle;
                model.IsFinalApprove = beforUpdateModel.IsFinalApprove;
                var result = await _unitOfWork.ContractAddendumRepository.Update(model);
                await _historyService.AddHistory(useer.ID, "ویرایش الحاقیه در قرارداد", model.Id, useer.FullName);
                result = _contractService.UpdateInAddendum(contractAddendum.Contract, user, contractAddendum.Id).Result.isSuccess;
                await _unitOfWork.Save();
                if (result)
                {  
                    return ("ویرایش الحاقیه موفقیت آمیز بود.", true);
                }
                return ("ویرایش الحاقیه با خطا روبرو شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی الحاقیه", false);
            }
        }
        internal async Task DeleteExceptinalContractAddendum(Guid addendumId)
        {
            try
            {
                await _unitOfWork.ContractAddendumRepository.DeleteExceptionalAddendum(addendumId);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        internal async Task CheckDoesContractHaveAnyAddendum(Guid ContractId)
        {
            try
            {
                var AllAddendumForAContract = await _unitOfWork.ContractAddendumRepository.GetAll(ContractId);
                if (AllAddendumForAContract == null || AllAddendumForAContract.Count == 0)
                {
                    var model = await _unitOfWork.ContractRepository.Get(ContractId);
                    model.HasAddendum = false;
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);

            }
        }
    }
}
