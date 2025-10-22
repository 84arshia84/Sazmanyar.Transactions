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
    internal class WfeContractAddendumService : IWfeContractAddendumService
    {
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUserService _userService;
        private IHistoryService _historyService;
        public WfeContractAddendumService(IErrorLoggerService errorLoggerService, IConfiguration configuration, IUserService userService ,IHistoryService historyService)
        {
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _userService = userService;
            _historyService = historyService;   
        }
        /// <summary>
        /// تایید قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> ApproveAddendumContract(Guid contractId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = contractId, Operation = 1, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "تایید الحاقیه قرارداد ", contractId, user.FullName);
                }
                catch
                {

                }
                return ("تایید موفقیت آمیز بود", true);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در تایید قرارداد", false);
            }
        }

        /// <summary>
        /// تمام الحاقیه هایی که کاربر می تواند ببیند
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetAllUserCanSeenIds(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
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
        /// <summary>
        /// تمام الحاقیه هایی که کاربر می تواند ببیند
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAllUserCanSeen(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
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
        /// <summary>
        /// تمام الحاقیه هایی که منتظر اقدام کاربر و یا بهش ارجاع شده است
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetAllWaitingForActionIds(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
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
        /// <summary>
        /// تمام الحاقیه هایی که منتظر اقدام کاربر و یا بهش ارجاع شده است
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify)
        {
            var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
            var wFETokenService = new WFETokenService(_configuration);
            var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
            var httpRequestService = new HttpRequestService();
            var baseUrl = _configuration["WFESettings:apiUrl"];
            var url = baseUrl + "WFEngine/GetCurentEntitiesForUser";
            var data = new { user.Id, AdditionalWhereClause = " en.IsDeleted = 0 " };
            var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
            var GetCurentEntitiesForUserData = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetCurentEntitiesForUserResponse);
            var waitingForActionIds = GetCurentEntitiesForUserData.ToList();
            return waitingForActionIds;
        }
        /// <summary>
        /// گرفتن داده سطح اول ماژول الحاقیه ها
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<GetFirstStageDto> GetFirstStage(string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
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
        /// <summary>
        /// ارجاع دادن الحاقیه
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> AssignAddendumContract(Guid contractId, string userFullQualify, List<Guid> userIds)
        {
            try
            {
                var users = await _userService.GetAll(_configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "AssignedEntity/AddAssignedEntity";
                var datas = new List<dynamic>();
                foreach (var item in userIds)
                {
                    var data = new { EntityId = contractId, UserId = item };
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
                            await _historyService.AddHistory(item, "ارجاع داده شده به", contractId, username.FullName);
                        }
                    }

                    var user = users.FirstOrDefault(x => x.FullQualifyName == userFullQualify);
                    if (user != null)
                    {
                        await _historyService.AddHistory(user.ID, "ارجاع داده شده توسط", contractId, user.FullName);
                    }

                }
                catch { }
                return ("ارجاع موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارحاع قرارداد", false);
            }
        }
        /// <summary>
        /// رد کردن الحاقیه
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> RejectAddendumContract(Guid contractId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = contractId, Operation = 2, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "رد در الحاقیه قرارداد", contractId, user.FullName);

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
        /// <summary>
        /// بازگشت الحاقیه ای که قبلا ارجاع داده شده بود
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> ReturnAddendumContract(Guid contractId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"AssignedEntity/Return?EntityId={contractId}";
                var data = new { };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "بازگشت الحاقیه قرارداد ", contractId, user.FullName);

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

        /// <summary>
        /// ارسال کردن الحاقیه ها
        /// </summary>
        /// <param name="contractIds"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> SendAddendumContract(List<Guid> contractIds, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                foreach (var contractId in contractIds)
                {
                    var url = baseUrl + $"WFEngine/Send?EntityId={contractId}";
                    await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                }
                try
                {
                    foreach (var contractId in contractIds)
                    {
                        await _historyService.AddHistory(user.ID, "ارسال الحاقیه قرارداد", contractId, user.FullName);
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
        /// <summary>
        /// کاربر برای هر الحاقیه چه عملیات هایی را می تواند انجام دهد 
        /// مثلا ارجاع
        /// ویرایش
        /// رد
        /// تایید
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
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
        /// <summary>
        /// جزئیات سطح الحاقیه
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<List<StageDetailsDto>> GetAddendumContractStageDetails(Guid contractId, string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                var url = baseUrl + $"WFEngine/GetAllStageDetailsFromEntityId?EntityId={contractId}";
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
        /// <summary>
        /// شخص ثبت کننده قرارداد سطح 1 فلو قرار می گیرد
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task InsertDefaultApprovers(string userFullQualify, Guid addendumId)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/InsertDefaultApprovers?EntityId={addendumId}";
                var data = new { };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Guid>> GetEntitiesAwaitingUserAction( string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingUserAction";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetAddendumRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var addendumRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetAddendumRole);
                var ids = addendumRoles.Select(x => x.Id).ToList();
                return ids;
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
                var wFEToken = await wFETokenService.GetWFEContractAddendumToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingApproval";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetAddendumRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var addendumRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetAddendumRole);
                var ids = addendumRoles.Select(x => x.Id).ToList();
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
