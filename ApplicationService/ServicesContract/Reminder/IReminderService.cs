using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Reminder
{
    public interface IReminderService
    {
        public Task<dynamic> GetReminderContractCounts(string fullQualifyName);
        public Task<dynamic> GetReminderAddendumCounts(string fullQualifyName);
        public Task<dynamic> GetReminderTransactionCounts(string fullQualifyName);
        public Task<dynamic> GetReminderInvoiceCounts(string fullQualifyName);
        public Task<dynamic> GetReminderFactorCounts(string fullQualifyName);
    }
}
