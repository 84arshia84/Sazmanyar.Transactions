using AppCore.Entities.User;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorStageRoles;
using ApplicationService.DtoModels.ReminderDtos;
using ApplicationService.DtoModels.WFEDto;
using ApplicationService.Services.WFEService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEFactor;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.WFEFactorService
{
    public class WfeFactorService : IWfeFactorService
    {
        private IUserService _userService;
        private IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IHistoryService _historyService;


        public WfeFactorService(IErrorLoggerService errorLoggerService,IUserService userService, IConfiguration configuration, IUnitOfWork unitOfWork,IHistoryService historyService
    )
        {
            _userService = userService;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;   
            _historyService = historyService;   

           
        }
        public async Task<(string message, bool isSucess)> ApproveFactor(Guid factorId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = factorId, Operation = 1, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "تایید فاکتور ", factorId, user.FullName);
                }
                catch(Exception ex)
                {
                    _errorLoggerService.SaveError(ex);
                }
                return ("تایید موفقیت آمیز بود", true);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در تایید قرارداد", false);
            }
        }

        public async Task<(string message, bool isSucess)> AssignFactor(Guid factorId, string userFullQualify, List<Guid> userIds)
        {

            try
            {
                
                var users = await _userService.GetAll(_configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "AssignedEntity/AddAssignedEntity";
                var datas = new List<dynamic>();
                foreach (var item in userIds)
                {
                    var data = new { EntityId = factorId, UserId = item };
                    datas.Add(data);
                }
                await httpRequestService.SendAsync(url, datas, wFEToken, HttpMethod.Post);
                try
                {
                    foreach (var item in userIds)
                    {
                        var username = users.FirstOrDefault(x => x.ID == item);
                        if (username != null)
                        {
                            await _historyService.AddHistory(item, "ارجاع داده شده به", factorId, username.FullName);
                        }
                    }
                    var user = users.FirstOrDefault(x => x.FullQualifyName == userFullQualify);
                    if (user != null)
                    {
                        await _historyService.AddHistory(user.ID, "ارجاع داده شده توسط", factorId, user.FullName);
                    }

                }
                 catch (Exception ex)
                {
                    _errorLoggerService.SaveError(ex);
                }
                
                return ("ارجاع موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارجاع قرارداد", false);
            }
        }

        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetCurentEntitiesForUser";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var AcceptableAction = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                return AcceptableAction;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<WFEGetCurentEntitiesForUserDto>();
            }
        }

        public async Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify)
        {

            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetAllEntitesForUserThatCanBeSeen";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                var GetAllUserCanSee = GetCurentEntitiesForUserData.Select(x => x.Id).ToList();
                return GetAllUserCanSee;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify)
        {

            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetAllEntitesForUserThatCanBeSeen";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                return GetCurentEntitiesForUserData;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetCurentEntitiesForUser";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                var waitingForActionIds = GetCurentEntitiesForUserData.Where(x => x.IsSendable == false).ToList();
                return waitingForActionIds;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<WFEGetCurentEntitiesForUserDto>();
            }

        }

        public async Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetCurentEntitiesForUser";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                var waitingForActionIds = GetCurentEntitiesForUserData.Where(x => x.IsSendable == false).Select(x => x.Id).ToList();
                return waitingForActionIds;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<List<StageDetailsDto>> GetFactorStageDetails(Guid factorId, string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                var url = baseUrl + $"WFEngine/GetAllStageDetailsFromEntityId?EntityId={factorId}";
                var GetStageDetails = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Get);
                var ContractStageDetail = JsonConvert.DeserializeObject<List<StageDetailsDto>>(GetStageDetails);
                return ContractStageDetail;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<StageDetailsDto>();
            }
        }

        public async Task<GetFirstStageDto> GetFirstStage(string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var getFirstStageUrl = baseUrl + "WFEngine/GetFirstStage";
                var getFirstStageResponse = await httpRequestService.SendAsync<object>(getFirstStageUrl, null, wFEToken, HttpMethod.Get);
                var getFirstStageDto = JsonConvert.DeserializeObject<GetFirstStageDto>(getFirstStageResponse);
                return getFirstStageDto;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task InsertDefaultApprovers(string userFullQualify, Guid factorId)
        {

            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/InsertDefaultApprovers?EntityId={factorId}";
                var data = new { };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(string message, bool isSucess)> RejectFactor(Guid factorId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = factorId, Operation = 2, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "رد فاکتور", factorId, user.FullName);

                }
                catch (Exception ex)
                {
                    _errorLoggerService.SaveError(ex);
                }
                return ("رد موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در رد قرارداد", false);
            }
        }

        public async Task<(string message, bool isSucess)> ReturnFactor(Guid factorId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"AssignedEntity/Return?EntityId={factorId}";
                var data = new { };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "بازگشت فاکتور ", factorId, user.FullName);

                }
                catch (Exception ex)
                {
                    _errorLoggerService.SaveError(ex);
                }
                return ("بازگشت موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بازگشت قرارداد", false);
            }
        }

        public async Task<(string message, bool isSucess)> SendFactor(List<Guid> factorIds, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                foreach (var factorId in factorIds)
                {
                    var url = baseUrl + $"WFEngine/Send?EntityId={factorId}";
                    await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                }
                try
                {
                    foreach (var contractId in factorIds)
                    {
                        await _historyService.AddHistory(user.ID, "ارسال  فاکتور", contractId, user.FullName);
                    }

                }
                catch (Exception ex)
                {
                    _errorLoggerService.SaveError(ex);
                }
                return ("ارسال موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارسال قرارداد", false);
            }
        }

        // New WFEngine API
        public async Task<List<Guid>> GetEntitiesAwaitingUserAction( string userFullQualify)
        {
            try
            { 

                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingUserAction";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetFactorRoles = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var factorRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetFactorRoles);
                var ids = factorRoles.Select(x => x.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }






        // New WFEngine API


        public async Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify)
        {

            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEFactorToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingApproval";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetFactorRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var factorRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetFactorRole);
                var ids = factorRoles.Select(x => x.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }
    }
}
