using AppCore.Entities.ContractsInformation.ServiceExplanations;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal static class ContractLastAmount
    {
        public static string CalculateContractLastAmount(List<ServiceExplanation> explanations, IErrorLoggerService errorLogger)
        {
			try
			{
                var result = string.Join(" , ",
                 explanations
                 .GroupBy(e => e.CurrencyID)
                 .Select(g =>
                     $"{g.Sum(e => (decimal)e.TotalAmount).ToString("N")} {g.First().Currency.Title} "
                 ));
                return result;
            }
			catch (Exception ex)
			{
				errorLogger.SaveError(ex);
                return string.Empty;
			}
        }
    }
}
