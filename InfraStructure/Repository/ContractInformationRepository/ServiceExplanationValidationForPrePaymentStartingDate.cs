using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Enums;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    public class ServiceExplanationValidationForPrePaymentStartingDate
    {
        private readonly AppDbContext _context;
        public ServiceExplanationValidationForPrePaymentStartingDate(AppDbContext context)
        {
            _context = context;
        }

        public async Task Validation(ServiceExplanation serviceExplanation)
        {
            try
            {

                {
                    var timeProfile = await _context.ContratTimeProfiles
                        .FirstOrDefaultAsync(tp => tp.ContractID == serviceExplanation.ContractID);

                    if (timeProfile != null
                        && timeProfile.ContractStartDate == null
                        && timeProfile.BasisForStartingProjectID != null)
                    {
                        var basis = await _context.BasisForStartingTheProjects
                            .Where(b => b.ID == timeProfile.BasisForStartingProjectID && !b.IsDeleted)
                            .Select(b => b.Title)
                            .FirstOrDefaultAsync();

                        var normTitle = basis?.Replace("‌", "").Replace(" ", "") ?? "";
                        var isPrepaymentBasis = normTitle.Contains("پیشپرداخت") || normTitle.Contains("پیش‌پرداخت");

                        if (isPrepaymentBasis)
                        {
                            timeProfile.ContractStartDate = serviceExplanation.ExplanationStartingDate;
                            _context.ContratTimeProfiles.Update(timeProfile);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
