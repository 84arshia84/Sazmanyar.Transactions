using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal static class ServiceExplanationFinancilaCalculationService
    {
        public static CalculatedSERequestedFinancialDto CalculateRequestedProperties(CUDSERequestedFinancialDto cUDSERequestedFinancialDto)
        {
            try
            {
                var result = new CalculatedSERequestedFinancialDto();
                if (cUDSERequestedFinancialDto.RequestedPercent != null)
                {
                    //Percent Added
                    result.CalculatedRequestedPercent = cUDSERequestedFinancialDto.RequestedPercent.Value;
                    result.CalculatedRequestedPrice = (result.CalculatedRequestedPercent * cUDSERequestedFinancialDto.TotalAmount) / 100;
                    //وقتی درصد میزنه، ممکنه حجم داشته باشیم یا نه
                    if (cUDSERequestedFinancialDto.ProgramVolume != null)
                    {
                        //حجم داریم
                        result.CalculatedRequestedVolume = (result.CalculatedRequestedPercent * cUDSERequestedFinancialDto.ProgramVolume.Value) / 100;
                    }
                    else
                    {
                        //حجم نداریم
                        result.CalculatedRequestedVolume = null;
                    }
                }
                if (cUDSERequestedFinancialDto.RequestedPrice != null)
                {
                    //Price Added
                    result.CalculatedRequestedPrice = cUDSERequestedFinancialDto.RequestedPrice.Value;
                    result.CalculatedRequestedPercent = (result.CalculatedRequestedPrice / cUDSERequestedFinancialDto.TotalAmount) * 100;
                    //وقتی مبلغ میزنه، ممکنه حجم داشته باشیم یا نه
                    if (cUDSERequestedFinancialDto.ProgramVolume != null)
                    {
                        //حجم داریم
                        result.CalculatedRequestedVolume = (result.CalculatedRequestedPrice * cUDSERequestedFinancialDto.ProgramVolume.Value) / cUDSERequestedFinancialDto.TotalAmount;
                    }
                    else
                    {
                        //حجم نداریم
                        result.CalculatedRequestedVolume = null;
                    }
                }
                if (cUDSERequestedFinancialDto.RequestedVolume != null)
                {
                    //Volume Added
                    //اگر حجم زده باشه، یعنی قطعا دوتای دیگه باید محاسبه بشه
                    result.CalculatedRequestedVolume = cUDSERequestedFinancialDto.RequestedVolume;
                    result.CalculatedRequestedPrice = (cUDSERequestedFinancialDto.RequestedVolume.Value * cUDSERequestedFinancialDto.TotalAmount) / cUDSERequestedFinancialDto.ProgramVolume.Value;
                    result.CalculatedRequestedPercent = (result.CalculatedRequestedVolume.Value / cUDSERequestedFinancialDto.ProgramVolume.Value) * 100;
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public static CalculatedSEApprovedFinancialDto CalculateApprovedProperties(SetSEApprovedFinancialDto setSEApprovedFinancialDto)
        {
            try
            {
                var result = new CalculatedSEApprovedFinancialDto();
                if (setSEApprovedFinancialDto.ApprovedPercent != null)
                {
                    //Percent Added
                    result.CalculatedApprovedPercent = setSEApprovedFinancialDto.ApprovedPercent.Value;
                    result.CalculatedApprovedPrice = (result.CalculatedApprovedPercent * setSEApprovedFinancialDto.TotalAmount) / 100;
                    //وقتی درصد میزنه، ممکنه حجم داشته باشیم یا نه
                    if (setSEApprovedFinancialDto.ProgramVolume != null)
                    {
                        //حجم داریم
                        result.CalculatedApprovedVolume = (result.CalculatedApprovedPercent * setSEApprovedFinancialDto.ProgramVolume.Value) / 100 /*(setSEApprovedFinancialDto.TotalAmount * 100)*/;
                    }
                    else
                    {
                        //حجم نداریم
                        result.CalculatedApprovedVolume = null;
                    }
                }
                if (setSEApprovedFinancialDto.ApprovedPrice != null)
                {
                    //Price Added
                    result.CalculatedApprovedPrice = setSEApprovedFinancialDto.ApprovedPrice.Value;
                    result.CalculatedApprovedPercent = (result.CalculatedApprovedPrice / setSEApprovedFinancialDto.TotalAmount) * 100;
                    //وقتی مبلغ میزنه، ممکنه حجم داشته باشیم یا نه
                    if (setSEApprovedFinancialDto.ProgramVolume != null)
                    {
                        //حجم داریم
                        result.CalculatedApprovedVolume = (result.CalculatedApprovedPercent * setSEApprovedFinancialDto.ProgramVolume.Value) / (setSEApprovedFinancialDto.TotalAmount * 100);
                    }
                    else
                    {
                        //حجم نداریم
                        result.CalculatedApprovedVolume = null;
                    }
                }
                if (setSEApprovedFinancialDto.ApprovedVolume != null)
                {
                    //Volume Added
                    //اگر حجم زده باشه، یعنی قطعا دوتای دیگه باید محاسبه بشه
                    result.CalculatedApprovedVolume = setSEApprovedFinancialDto.ApprovedVolume;
                    result.CalculatedApprovedPrice = (setSEApprovedFinancialDto.ApprovedVolume.Value * setSEApprovedFinancialDto.TotalAmount) / setSEApprovedFinancialDto.ProgramVolume.Value;
                    result.CalculatedApprovedPercent = (result.CalculatedApprovedPrice / setSEApprovedFinancialDto.TotalAmount) * 100;
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
