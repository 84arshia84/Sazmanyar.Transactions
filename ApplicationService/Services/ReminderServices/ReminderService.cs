using AppCore.Entities.FactorInformation.Factors;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceStageRolesDtos;
using ApplicationService.DtoModels.ReminderDtos;
using ApplicationService.Services.WFEService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Reminder;
using ApplicationService.ServicesContract.WFEContract;
using ApplicationService.ServicesContract.WFEFactor;
using ApplicationService.ServicesContract.WFEInvoice;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ReminderServices
{
    public class ReminderService : IReminderService
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private IWfeContractService _wfeContractService;
        private IWfeTransactionExecutionRequestService _wfeTransactionExecutionRequestService;
        private IWfeContractAddendumService _wfeContractAddendumService;
        private IWfeFactorService _wfeFactorService;
        private IWfeInvoiceService _wfeInvoiceService;
        public ReminderService(
            IErrorLoggerService errorLoggerService,
            IWfeContractService wfeContractService,
            IWfeTransactionExecutionRequestService wfeTransactionExecutionRequestService,
            IWfeContractAddendumService wfeContractAddendumService,
            IWfeFactorService wfeFactorService,
            IWfeInvoiceService wfeInvoiceService
            )
        {
            _errorLoggerService = errorLoggerService;
            _wfeContractService = wfeContractService;
            _wfeTransactionExecutionRequestService = wfeTransactionExecutionRequestService;
            _wfeContractAddendumService = wfeContractAddendumService;
            _wfeFactorService = wfeFactorService;
            _wfeInvoiceService = wfeInvoiceService;
        }

        public async Task<dynamic> GetReminderAddendumCounts(string fullQualifyName)
        {
            try
            {
                var Addendum = await _wfeContractAddendumService.GetEntitiesAwaitingUserAction(fullQualifyName);
                var Addendums = await _wfeContractAddendumService.GetEntitiesAwaitingApproval(fullQualifyName);

                dynamic data = new
                {
                    PendingAction = Addendum.Count(),
                    PendingApproval = Addendums.Count()
                };
                return data;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<dynamic> GetReminderContractCounts(string fullQualifyName)
        {
            try
            {
                var Contracts = await _wfeContractService.GetEntitiesAwaitingUserAction(fullQualifyName);
                var Contract = await _wfeContractService.GetEntitiesAwaitingApproval(fullQualifyName);

                dynamic data = new
                {
                    PendingAction = Contracts.Count(),
                    PendingApproval = Contract.Count()
                };
                return data;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<dynamic> GetReminderFactorCounts(string fullQualifyName)
        {
            try
            {
                var Factors = await _wfeFactorService.GetEntitiesAwaitingUserAction(fullQualifyName);
                var Factor = await _wfeFactorService.GetEntitiesAwaitingApproval(fullQualifyName);

                dynamic data = new
                {
                    PendingAction = Factors.Count(),
                    PendingApproval = Factor.Count()
                };
                return data;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<dynamic> GetReminderInvoiceCounts(string fullQualifyName)
        {
            try
            {
                var Invoice = await _wfeInvoiceService.InvoiceGetEntitiesAwaitingUserAction(fullQualifyName);
                var Invoices = await _wfeInvoiceService.InvoiceGetEntitiesAwaitingApproval(fullQualifyName);

                dynamic data = new
                {
                    PendingAction = Invoice.Count(),
                    PendingApproval = Invoices.Count()
                };
                return data;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task<dynamic> GetReminderTransactionCounts(string fullQualifyName)
        {
            try
            {
                var Transaction = await _wfeTransactionExecutionRequestService.GetEntitiesAwaitingUserAction(fullQualifyName);
                var Transactions = await _wfeTransactionExecutionRequestService.GetEntitiesAwaitingApproval(fullQualifyName);

                dynamic data = new
                {
                    PendingAction = Transaction.Count(),
                    PendingApproval = Transactions.Count()
                };
                return data;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }



 

    }
}
