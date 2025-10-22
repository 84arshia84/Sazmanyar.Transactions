using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.FactorStageRoles;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceStageRolesDtos;
using ApplicationService.DtoModels.ReminderDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.DtoModels.WFEDto;
using ApplicationService.Services.WFEService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.Reminder;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEInvoice;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.WFEInvoiceServices
{
    internal class WfeInvoiceService : IWfeInvoiceService
    {
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUserService _userService;
        private IHistoryService _historyService;
        public WfeInvoiceService(IErrorLoggerService errorLoggerService, IConfiguration configuration,
            IUserService userService , IHistoryService historyService)
        {
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _userService = userService;
            _historyService = historyService;   
        }
        public async Task<(string message, bool isSucess)> ApproveInvoice(Guid invoiceId, string userFullQualify, bool checkPaymentIsComplet, int stage)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                // get wfe token
                if (stage == 4)
                {
                    if (!checkPaymentIsComplet)
                    {
                        return ("تایید مبالغ پرداخت شده انجام شد", true);
                    }
                }
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);

                // send request to WFE/ApproveOrReject
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var sendUrl = baseUrl + $"WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = invoiceId, Operation = 1, SelectedStageIDForReject = Guid.Empty };
                var sendResponse = await httpRequestService.SendAsync<object>(sendUrl, data, wFEToken, HttpMethod.Put);
                try
                {

                    await _historyService.AddHistory(user.ID, "تایید صورت وضعیت ",invoiceId, user.FullName);

                }
                catch
                {

                }
                return ("تایید موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در تایید صورت وضعیت", false);
            }
        }

        public async Task<(string message, bool isSucess)> AssignInvoice(Guid invoiceId, string userFullQualify, List<Guid> userIds)
        {
            try
            {
                var users = await _userService.GetAll(_configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "AssignedEntity/AddAssignedEntity";
                var datas = new List<dynamic>();
                foreach (var item in userIds)
                {
                    var data = new { EntityId = invoiceId, UserId = item };
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
                            await _historyService.AddHistory(item, "ارجاع داده شده به", invoiceId, username.FullName);
                        }
                    }
                    var user = users.FirstOrDefault(x => x.FullQualifyName == userFullQualify);
                    if (user != null)
                    {
                        await _historyService.AddHistory(user.ID, "ارجاع داده شده توسط", invoiceId, user.FullName);
                    }
    

                }
                catch { }
                return ("ارجاع موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ارجاع صورت وضعیت", false);
            }
        }
        /// <summary>
        /// کاربر برای هر صورت وضعیت چه عملیات هایی را می تواند انجام دهد 
        /// مثلا ارجاع
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAcceptableActionsForEntity(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
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
        /// تمام صورت وضعیت هایی که منتظر اقدام کاربر و یا بهش ارجاع شده است
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<WFEGetCurentEntitiesForUserDto>> GetAllWaitingForAction(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
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
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
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
        /// جزئیات سطح صورت وضعیت
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<List<StageDetailsDto>> GetInvoiceStageDetails(Guid invoiceId, string userFullQualify)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                var url = baseUrl + $"WFEngine/GetAllStageDetailsFromEntityId?EntityId={invoiceId}";
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
        /// شخص ثبت کننده صورت وضعیت سطح 1 فلو قرار می گیرد
        /// </summary>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task InsertDefaultApprovers(string userFullQualify, Guid invoiceId)
        {
            try
            {
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/InsertDefaultApprovers?EntityId={invoiceId}";
                var data = new { };
                var GetCurentEntitiesForUserResponse = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        /// گرفتن نقش های صورت وضعیت در هرسطح از فلو
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        public async Task<List<InvoiceStageRolseDto>> InvoiceStageRoles(Guid invoiceId, string userFullQualify)
        {
            try
            {
                WFETokenService wFETokenService = new WFETokenService(_configuration);
                string wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                HttpRequestService httpRequestService = new HttpRequestService();
                string baseUrl = _configuration["WFESettings:apiUrl"];
                var stagerole= _configuration["WFESettings:parameters:stageroles"];
                var url = baseUrl + $"WFEngine/GetFiledsFromEntityId?EntityId={invoiceId}";
                var data = new { };
                var GetInvoiceRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Get);
                var InvoiceRoles = JsonConvert.DeserializeObject<Dictionary<string, List<InvoiceStageRolseDto>>>(GetInvoiceRole);
                return InvoiceRoles.Any() ? InvoiceRoles[stagerole] : new List<InvoiceStageRolseDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceStageRolseDto>();
            }
        }







        /// <summary>
        /// رد کردن صورت وضعیت
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> RejectInvoice(Guid invoiceId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + "WFEngine/ApproveOrRejectEntities";
                var data = new { EntityId = invoiceId, Operation = 2, SelectedStageIDForReject = Guid.Empty };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "رد صورت وضعیت", invoiceId, user.FullName);

                }
                catch
                {

                }
                return ("رد موفقیت آمیز بود", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در رد صورت وضعیت", false);
            }
        }
        /// <summary>
        /// بازگشت صورت وضعیتی که قبلا ارجاع داده شده بود
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> ReturnInvoice(Guid invoiceId, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"AssignedEntity/Return?EntityId={invoiceId}";
                var data = new { };
                await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                try
                {
                    await _historyService.AddHistory(user.ID, "بازگشت صورت وضعیت ", invoiceId, user.FullName);

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
                return ("خطا در بازگشت صورت وضعیت", false);
            }
        }
        /// <summary>
        /// ارسال کردن صورت وضعیت ها
        /// </summary>
        /// <param name="invoiceIds"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSucess)> SendInvoice(List<Guid> invoiceIds, string userFullQualify)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);
                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var data = new { };
                foreach (var contractId in invoiceIds)
                {
                    var url = baseUrl + $"WFEngine/Send?EntityId={contractId}";
                    await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Put);
                }
                try
                {
                    foreach (var contractId in invoiceIds)
                    {
                        await _historyService.AddHistory(user.ID, "ارسال  صورت وضعیت", contractId, user.FullName);
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
                return ("خطا در ارسال صورت وضعیت", false);
            }
        }
        /// <summary>
        /// این صورت وضعیت منتظر اقدام من هست ؟
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userFullQualify"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> WaitForMe(Guid invoiceId, string userFullQualify)
        {
            try
            {
                var ids = await GetAllWaitingForActionIds(userFullQualify);
                if (ids.Count > 0)
                {
                    if (ids.Any(x => x == invoiceId))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }


        public async Task<List<Guid>> InvoiceGetEntitiesAwaitingUserAction(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingUserAction";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetInvoiceRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var invoiceRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetInvoiceRole);
                var ids = invoiceRoles.Select(x => x.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }


        public async Task<List<Guid>> InvoiceGetEntitiesAwaitingApproval(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingApproval";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetInvoiceRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var invoiceRoles = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetInvoiceRole);
                var ids = invoiceRoles.Select(x => x.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<Guid>();
            }
        }
        public async Task<List<WFEGetCurentEntitiesForUserDto>> InvoiceGetEntitiesAwaitingApprovalDto(string userFullQualify)
        {
            try
            {
                var user = _userService.GetByFullQualifyName(userFullQualify, _configuration["ConnectionStrings:DbConnection"]);

                var wFETokenService = new WFETokenService(_configuration);
                var wFEToken = await wFETokenService.GetWFEInvoiceToken(userFullQualify);
                var httpRequestService = new HttpRequestService();
                var baseUrl = _configuration["WFESettings:apiUrl"];
                var url = baseUrl + $"WFEngine/GetEntitiesAwaitingApproval";
                var data = new { AdditionalWhereClause = "en.IsDeleted = 0" };
                var GetInvoiceRole = await httpRequestService.SendAsync(url, data, wFEToken, HttpMethod.Post);
                var Access = JsonConvert.DeserializeObject<List<WFEGetCurentEntitiesForUserDto>>(GetInvoiceRole);
                return Access;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<WFEGetCurentEntitiesForUserDto>();
            }
        }
    }
}
