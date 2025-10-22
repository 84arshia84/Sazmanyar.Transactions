using AppCore.Entities.User;
using AppCore.UnitOfWork;
using ApplicationService.Common;
using AppCore.Enums;
using ApplicationService.DtoModels.FactorDtos.Factor;
using ApplicationService.Mapper.FactorMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorAccessGroups;
using ApplicationService.ServicesContract.FactorInformation;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEFactor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.FactorInformation.Factors;

namespace ApplicationService.Services.FactorInformationService
{
    internal class FactorService : IFactorService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUserService _userService;
        private IFactorFinancialDetaileService _factorFinancialDetaileService;
        private IFactorTimeProfileService _factorTimeProfileService;
        private IFactorNettingProcessItemService _factorNettingProcessItemService;
        private IFactorServiceExplanationService _factorExplanationService;
        private IWfeFactorService _wfFactorService;
        private IFactorAccessGroupFilterService _factorAccessGroupFilter;
        private IHistoryService _historyService;
        public FactorService(
            IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IConfiguration configuration,
            IUserService userService, IFactorFinancialDetaileService factorFinancialDetaileService,
            IFactorTimeProfileService factorTimeProfileService, IFactorNettingProcessItemService factorNettingProcessItem,
            IFactorServiceExplanationService factorServiceExplanation, IWfeFactorService wfeFactorService,
            IFactorAccessGroupFilterService factorAccessGroupFilter,
            IHistoryService historyService
            )
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _userService = userService;
            _factorFinancialDetaileService = factorFinancialDetaileService;
            _factorTimeProfileService = factorTimeProfileService;
            _factorNettingProcessItemService = factorNettingProcessItem;
            _factorExplanationService = factorServiceExplanation;
            _wfFactorService = wfeFactorService;
            _factorAccessGroupFilter = factorAccessGroupFilter;
            _historyService = historyService;
        }
        public async Task<(string message, bool isSuccess)> Add(FactorAddDto factor, string userName)
        {
            var publicFactorId = Guid.Empty;
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var entity = FactorAutoMapperProfile.DtoToEntityAdd(factor, user.ID, _errorLoggerService);
                publicFactorId = entity.Id;
                var wfe = await _wfFactorService.GetFirstStage(user.FullQualifyName);
                if (wfe != null)
                {
                    entity.CurrentStageId = wfe.Id;
                    entity.CurrentStatusTitle = $"در انتظار ارسال کاربر {user.FullQualifyName}";
                    entity.LastActionTitle = $"ثبت شده توسط کاربر {user.FullQualifyName}";
                }
                // بررسی تکراری بودن کد فاکتور
                var duplicate = await _unitOfWork.FactorRepository.GetByFactorNumber(entity.FactorNumber);
                if (duplicate != null)
                    return ("کد فاکتور تکراری است", false);
                var FactorResult = await _unitOfWork.FactorRepository.Add(entity);
                var FinancialResult = await _factorFinancialDetaileService.Add(factor.FactorFinancialDetaile, entity.FactorFinancialDetaileId, publicFactorId);
                var TimeProfileResult = await _factorTimeProfileService.Add(factor.FactorTimeProfile, entity.FactorTimeProfileId, publicFactorId);
                await _unitOfWork.Save();
                if (FactorResult && FinancialResult && TimeProfileResult)
                {
                    await _wfFactorService.InsertDefaultApprovers(user.FullQualifyName, entity.Id);
                    if (factor.FactorServiceExplanation.Count == 0)
                    {
                        await DeleteExceptinalFactor(publicFactorId);
                        return ("فاکتور بدون شرح اقلام ثبت نمی شود", false);
                    }
                    var explenationResult = await _factorExplanationService.Add(factor.FactorServiceExplanation, publicFactorId);
                    if (!explenationResult)
                    {
                        await DeleteExceptinalFactor(publicFactorId);
                        return ("ثبت شرح اقلام فاکتور با خطا مواجه شد", false);
                    }
                    var nettingProccess = await _factorNettingProcessItemService.AddDefaultFactorData(factor.FactorFinancialDetaile, factor.FactorServiceExplanation, publicFactorId, user.ID);
                    if (!nettingProccess)
                    {
                        return ("خطا در محاسبات خالص سازی فاکتور", true);
                    }
                    await _historyService.AddHistory(user.ID, "افزودن  فاکتور", entity.Id, user.FullName);
                    await _unitOfWork.Save();
                    return ("فاکتور با موفقیت ثبت شد", true);
                }
                return ("خطا در ثبت اطالاعات پایه فاکتور", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                await DeleteExceptinalFactor(publicFactorId);
                return ("خطا در ثبت فاکتور", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid factorId, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                await _historyService.AddHistory(user.ID, "حذف قرارداد", factorId, user.FullName);
                var result = await _unitOfWork.FactorRepository.Delete(factorId, user.ID);

                if (!result)
                {
                    return ("حذف با خطا مواجه شد", false);
                }
                await _unitOfWork.Save();
                return ("حذف موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف کردن فاکتور", false);
            }
        }

        public async Task<FactorGetDto> Get(Guid factorId)
        {
            try
            {
                return FactorAutoMapperProfile.EntityToDto(await _unitOfWork.FactorRepository.Get(factorId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorGetDto();
            }
        }

        public async Task<List<FactorGetDto>> GetAll(string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var FactorIdsIds = await _wfFactorService.GetAllUserCanSeenIds(userName);
                var filteredData = await _factorAccessGroupFilter.FilterFactor(await _unitOfWork.FactorRepository.GetAll(FactorIdsIds), FactorIdsIds, user.ID);
                if (filteredData.Any())
                {
                    var dtos = FactorAutoMapperProfile.EntitiesToDtos(filteredData, _errorLoggerService);
                    return dtos.AsQueryable().OrderDefault().ToList();
                }
                return new List<FactorGetDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorGetDto>();
            }
        }

        public async Task<List<FactorGetDto>> GetAllMine(string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                // return FactorAutoMapperProfile.EntitiesToDtos(await _unitOfWork.FactorRepository.GetAllMine(user.ID), _errorLoggerService);
                var entities = await _unitOfWork.FactorRepository.GetAllMine(user.ID);
                var dtos = FactorAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
                return dtos.AsQueryable().OrderDefault().ToList();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorGetDto>();
            }
        }

        public async Task<List<FactorGetDto>> GetAllWaitForAction(string userName)
        {
            try
            {
                var FactorIdsIds = await _wfFactorService.GetEntitiesAwaitingUserAction(userName);
                var entities = await _unitOfWork.FactorRepository.GetAllWaitForAction(FactorIdsIds);
                var dtos = FactorAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
                return dtos.ToList();
            }
            catch (Exception ex)
            {

                return new List<FactorGetDto>();
            }
        }
        public async Task<List<FactorGetDto>> GetAllWaitForApprove(string userName)
        {
            try
            {
                var FactorIdsIds = await _wfFactorService.GetEntitiesAwaitingApproval(userName);
                var entities = await _unitOfWork.FactorRepository.GetAllWaitForAction(FactorIdsIds);
                var dtos = FactorAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
                return dtos.ToList();
            }
            catch (Exception ex)
            {

                return new List<FactorGetDto>();
            }
        }

        public async Task<List<FactorGetDto>> GetAllWithFinalApprove()
        {
            try
            {

                var entites = await _unitOfWork.FactorRepository.GetAllWithFinalApprove();
                var dtos = FactorAutoMapperProfile.EntitiesToDtos(entites, _errorLoggerService);
                return dtos.AsQueryable().OrderDefault().ToList();

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<FactorGetDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(FactorUpdateDto factor, string username)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(username, _configuration["ConnectionStrings:DbConnection"]);
                var lastData = await _unitOfWork.FactorRepository.Get(factor.Id);
                var entity = FactorAutoMapperProfile.DtoToEntityUpdate(factor, lastData, _errorLoggerService);
                var FactorResult = await _unitOfWork.FactorRepository.Update(entity);
                var TimeProfileResult = await _factorTimeProfileService.Update(factor.FactorTimeProfile);
                var FinancialResult = await _factorFinancialDetaileService.Update(factor.FactorFinancialDetaile);
                await _unitOfWork.Save();
                if (FactorResult && TimeProfileResult && FinancialResult)
                {
                    var explenationResult = await _factorExplanationService.Update(factor.FactorServiceExplanation);
                    if (!explenationResult)
                    {
                        return ("ویرایش شرح اقلام فاکتور با خطا مواجه شد", false);
                    }
                    await _factorNettingProcessItemService.UpdateDefaultFactorData(factor.FactorFinancialDetaile, factor.Id, factor.FactorServiceExplanation);
                    await _unitOfWork.Save();
                    await _historyService.AddHistory(user.ID, "ویرایش فاکتور", entity.Id, user.FullName);
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return ("ویرایش با خطا مواجه شد", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی فاکتور", false);
            }
        }
        internal async Task DeleteExceptinalFactor(Guid factorId)
        {
            try
            {
                await _unitOfWork.FactorRepository.DeleteExceptinalFactor(factorId);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        // بالای فایل:

public async Task<List<FactorGetDto>> Search(string userName, int situation, string? term)
{
    try
    {
        var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);

        if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.Mine)
        {
            var entities = await _unitOfWork.FactorRepository.SearchMineAsync(user.ID, term);
            var dtos = FactorAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
            return dtos.AsQueryable().OrderDefault().ToList(); // Id DESC
        }

        if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForAction)
        {
            var ids = await _wfFactorService.GetAllWaitingForActionIds(userName);
            if (ids?.Any() != true) return new();
            var entities = await _unitOfWork.FactorRepository.SearchWaitForActionAsync(ids, term);
            var dtos = FactorAutoMapperProfile.EntitiesToDtos(entities, _errorLoggerService);
            return dtos.AsQueryable().OrderDefault().ToList();
        }

        if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.All)
        {
            var canSeeIds = await _wfFactorService.GetAllUserCanSeenIds(userName);
            var entities = await _unitOfWork.FactorRepository.SearchAllAsync(term);

                    var filtered = await _factorAccessGroupFilter.FilterFactor(entities, canSeeIds, user.ID);
            if(filtered == null)
                filtered = new List<Factor>();
            var dtos = FactorAutoMapperProfile.EntitiesToDtos(filtered, _errorLoggerService);
            return dtos.AsQueryable().OrderDefault().ToList();
        }

        // FinalApprove (اختیاری):
        // var fa = await _unitOfWork.FactorRepository.SearchWithFinalApproveAsync(term);
        // var faDtos = FactorAutoMapperProfile.EntitiesToDtos(fa, _errorLoggerService);
        // return faDtos.AsQueryable().OrderDefault().ToList();

        return new();
    }
    catch (Exception ex)
    {
        _errorLoggerService.SaveError(ex);
        return new();
    }
}


    }
}
