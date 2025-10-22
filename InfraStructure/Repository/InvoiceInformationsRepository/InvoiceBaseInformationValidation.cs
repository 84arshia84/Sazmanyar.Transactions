using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Enums;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    public class InvoiceBaseInformationValidation
    {
        private readonly AppDbContext _context;
        public InvoiceBaseInformationValidation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(string message, bool isSuccess, Guid invoiceBaseInformationId)> Validation(InvoiceBaseInformation invoiceBaseInformation)
        {
            try
            {
                var prepaymentGuid = InvoiceTypesEnum.prepayment.GetGuid();
                if (invoiceBaseInformation.InvoiceTypeId == prepaymentGuid)
                {
                    var timeProfile = await _context.ContratTimeProfiles
                        .FirstOrDefaultAsync(tp => tp.ContractID == invoiceBaseInformation.ContractId);

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
                            timeProfile.ContractStartDate = invoiceBaseInformation.InsertDate;
                            _context.ContratTimeProfiles.Update(timeProfile);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return ("InvoiceBaseInformation added successfully", true, invoiceBaseInformation.Id);
            }
            catch (Exception ex)
            {
                return ($"Error occurred: {ex.Message}", false, Guid.Empty);
            }
        }
    }
}
