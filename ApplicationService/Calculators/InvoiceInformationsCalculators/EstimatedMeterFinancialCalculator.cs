using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Calculators.InvoiceInformationsCalculators
{
    internal static class EstimatedMeterFinancialCalculator
    {
        public static CalculatedEMRequestedFinancialDto CalculateRequestedProperties(CUDEMRequestedFinancialDto cUDEMRequestedFinancialDto)
        {
            var result = new CalculatedEMRequestedFinancialDto();
            if (cUDEMRequestedFinancialDto.RequestedPercent != null)
            {
                //Percent Added
                result.CalculatedRequestedPercent = cUDEMRequestedFinancialDto.RequestedPercent.Value;
                result.CalculatedRequestedPrice = (result.CalculatedRequestedPercent * cUDEMRequestedFinancialDto.TotalAmount) / 100;
                //وقتی درصد میزنه، ممکنه حجم داشته باشیم یا نه
                if (cUDEMRequestedFinancialDto.ProgramVolume != null)
                {
                    //حجم داریم
                    result.CalculatedRequestedVolume = (result.CalculatedRequestedPercent * cUDEMRequestedFinancialDto.ProgramVolume.Value) / (cUDEMRequestedFinancialDto.TotalAmount * 100);
                }
                else
                {
                    //حجم نداریم
                    result.CalculatedRequestedVolume = null;
                }
            }
            if (cUDEMRequestedFinancialDto.RequestedPrice != null)
            {
                //Price Added
                result.CalculatedRequestedPrice = cUDEMRequestedFinancialDto.RequestedPrice.Value;
                result.CalculatedRequestedPercent = (result.CalculatedRequestedPrice / cUDEMRequestedFinancialDto.TotalAmount) * 100;
                //وقتی مبلغ میزنه، ممکنه حجم داشته باشیم یا نه
                if (cUDEMRequestedFinancialDto.ProgramVolume != null)
                {
                    //حجم داریم
                    result.CalculatedRequestedVolume = (result.CalculatedRequestedPercent * cUDEMRequestedFinancialDto.ProgramVolume.Value) / (cUDEMRequestedFinancialDto.TotalAmount * 100);
                }
                else
                {
                    //حجم نداریم
                    result.CalculatedRequestedVolume = null;
                }
            }
            if (cUDEMRequestedFinancialDto.RequestedVolume != null)
            {
                //Volume Added
                //اگر حجم زده باشه، یعنی قطعا دوتای دیگه باید محاسبه بشه
                result.CalculatedRequestedVolume = cUDEMRequestedFinancialDto.RequestedVolume;
                result.CalculatedRequestedPrice = (cUDEMRequestedFinancialDto.RequestedVolume.Value * cUDEMRequestedFinancialDto.TotalAmount) / cUDEMRequestedFinancialDto.ProgramVolume.Value;
                result.CalculatedRequestedPercent = (result.CalculatedRequestedPrice / cUDEMRequestedFinancialDto.TotalAmount) * 100;
            }
            return result;
        }
        public static CalculatedEMApprovedFinancialDto CalculateApprovedProperties(SetEMApprovedFinancialDto setEMApprovedFinancialDto)
        {
            var result = new CalculatedEMApprovedFinancialDto();
            if (setEMApprovedFinancialDto.ApprovedPercent != null)
            {
                //Percent Added
                result.CalculatedApprovedPercent = setEMApprovedFinancialDto.ApprovedPercent.Value;
                result.CalculatedApprovedPrice = (result.CalculatedApprovedPercent * setEMApprovedFinancialDto.TotalAmount) / 100;
                //وقتی درصد میزنه، ممکنه حجم داشته باشیم یا نه
                if (setEMApprovedFinancialDto.ProgramVolume != null)
                {
                    //حجم داریم
                    result.CalculatedApprovedVolume = (result.CalculatedApprovedPercent * setEMApprovedFinancialDto.ProgramVolume.Value) / (setEMApprovedFinancialDto.TotalAmount * 100);
                }
                else
                {
                    //حجم نداریم
                    result.CalculatedApprovedVolume = null;
                }
            }
            if (setEMApprovedFinancialDto.ApprovedPrice != null)
            {
                //Price Added
                result.CalculatedApprovedPrice = setEMApprovedFinancialDto.ApprovedPrice.Value;
                result.CalculatedApprovedPercent = (result.CalculatedApprovedPrice / setEMApprovedFinancialDto.TotalAmount) * 100;
                //وقتی مبلغ میزنه، ممکنه حجم داشته باشیم یا نه
                if (setEMApprovedFinancialDto.ProgramVolume != null)
                {
                    //حجم داریم
                    result.CalculatedApprovedVolume = (result.CalculatedApprovedPercent * setEMApprovedFinancialDto.ProgramVolume.Value) / (setEMApprovedFinancialDto.TotalAmount * 100);
                }
                else
                {
                    //حجم نداریم
                    result.CalculatedApprovedVolume = null;
                }
            }
            if (setEMApprovedFinancialDto.ApprovedVolume != null)
            {
                //Volume Added
                //اگر حجم زده باشه، یعنی قطعا دوتای دیگه باید محاسبه بشه
                result.CalculatedApprovedVolume = setEMApprovedFinancialDto.ApprovedVolume;
                result.CalculatedApprovedPrice = (setEMApprovedFinancialDto.ApprovedVolume.Value * setEMApprovedFinancialDto.TotalAmount) / setEMApprovedFinancialDto.ProgramVolume.Value;
                result.CalculatedApprovedPercent = (result.CalculatedApprovedPrice / setEMApprovedFinancialDto.TotalAmount) * 100;
            }
            return result;
        }
    }
}
