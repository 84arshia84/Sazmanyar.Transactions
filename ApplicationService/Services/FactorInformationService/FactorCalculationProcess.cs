using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.FactorInformationService
{
    internal static class FactorCalculationProcess
    {
        public static decimal CalculatedNettedAmount(List<FactorNettingProcessItem> factorNettingProcessItems,decimal serviceExplanationValues,Guid currencyId)
        {
			try
			{
                decimal result = 0;
                decimal deductedPercentage = factorNettingProcessItems.Where(npi => npi.IsDeduction && npi.CurrencyId==currencyId).Sum(npi => npi.Percentage);
                decimal addedPercentage = factorNettingProcessItems.Where(npi => !npi.IsDeduction && npi.CurrencyId == currencyId).Sum(npi => npi.Percentage);
                decimal deductedPrice = (deductedPercentage * serviceExplanationValues) / 100;
                decimal addedPrice = (addedPercentage * serviceExplanationValues) / 100;
                result = serviceExplanationValues - deductedPrice + addedPrice;
                return result;
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
