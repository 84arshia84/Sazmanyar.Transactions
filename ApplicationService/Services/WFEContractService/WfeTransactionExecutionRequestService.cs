using AppCore.Entities.User;
using ApplicationService.DtoModels.WFEDto;
using ApplicationService.Services.WFEService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.WFEContractService
{
    public class WfeTransactionExecutionRequestService : IWfeTransactionExecutionRequestService
    {
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUserService _userService;
        private IHistoryService _historyService;
        public WfeTransactionExecutionRequestService(IErrorLoggerService errorLoggerService, IConfiguration configuration, IUserService userService,IHistoryService historyService)
        {
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _userService = userService;
            _historyService = historyService;   
        }
        public async Task<(string message, bool isSucess)> ApproveTransaction(Guid transactionId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = transactionId, Operation = 1, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {

                    await _historyService.AddHistory(user.ID, "تایید درخواست برگزاری معامله ", transactionId, user.FullName);

                }
                catch
                {

                }
                return ("تایید موفقیت آمیز بود", true);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در تایید ", false);
            }
        }

        public async Task<(string message, bool isSucess)> AssignTransaction(Guid transactionId, string userFullQualify, List<Guid> userIds)
        {
            try
            {
                var users = await _userService.GetAll(_configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "AssignedEntity/AddAssignedEntity";
                var datas = new List<dynamic>();
                foreach (var item in userIds)
                {
                    var data = new { EntityId = transactionId, UserId = item };
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
                            await _historyService.AddHistory(item, "ارجاع داده شده به", transactionId, username.FullName);
                        }

                    }

                    var user = users.FirstOrDefault(x => x.FullQualifyName == userFullQualify);
                    if (user != null)
                    {
                        await _historyService.AddHistory(user.ID, "ارجاع داده شده توسط", transactionId, user.FullName);
                    }

                }
                catch { }
                return ("ارجاع موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارجاع ", false);
            }
        }

        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/GetCurentEntitiesForUser";
                var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
                var waitingForActionIds = GetCurentEntitiesForUserData.ToList();
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
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
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

        public async Task<GetFirstStageDto> GetFirstStage(string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
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

        public async Task<List<StageDetailsDto>> GetTransactionStageDetails(Guid transactionId, string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                var url = baseUrl + $"WFEngine/GetAllStageDetailsFromEntityId?EntityId={transactionId}";
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

        public async Task InsertDefaultApprovers(string userFullQualify, Guid transactionId)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/InsertDefaultApprovers?EntityId={transactionId}";
                var data = new { };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(string message, bool isSucess)> RejectTransaction(Guid transactionId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = transactionId, Operation = 2, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "رد درخواست برگزاری معامله", transactionId, user.FullName);

                }
                catch
                {

                }
                return ("رد موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در رد قرارداد", false);
            }
        }

        public async Task<(string message, bool isSucess)> ReturnTransaction(Guid transactionId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"AssignedEntity/Return?EntityId={transactionId}";
                var data = new { };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "بازگشت درخواست برگزاری معامله ", transactionId, user.FullName);

                }
                catch
                {
                    throw new Exception();
                }
                return ("بازگشت موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بازگشت قرارداد", false);
            }
        }

        public async Task<(string message, bool isSucess)> SendTransaction(List<Guid> transactionIds, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFETransactionToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                foreach (var transactionId in transactionIds)
                {
                    var url = baseUrl + $"WFEngine/Send?EntityId={transactionId}";
                    await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                }
                try
                {
                    foreach (var transactionId in transactionIds)
                    {
                        await _historyService.AddHistory(user.ID, "ارسال درخواست برگزاری معامله", transactionId, user.FullName);
                    }

                }
                catch (Exception)
                {

                }
                return ("ارسال موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارسال قرارداد", false);
            }
        }
        public async Task<List<Guid>> GetEntitiesAwaitingUserAction(string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingUserAction";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetTransRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var transRoles = JsonConvert.DeserializeObject<List<Guid>>(GetTransRole);
                return transRoles;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }


        public async Task<List<Guid>> GetEntitiesAwaitingApproval(string userFullQualify)
        {

            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingApproval";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetTransRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var transRoles = JsonConvert.DeserializeObject<List<Guid>>(GetTransRole);
                return transRoles;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }
    }
}
