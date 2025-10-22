using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ApplicationService.DtoModels.PwaDtos;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    internal static class ServiceExplanationFinancialMapper
    {
        public static List<GetAllCSFDto> EntitiesToGetAllCFSDtos(List<ServiceExplanation> serviceExplanationEntities, List<ServiceExplanationFinancial> serviceExplanationFinancialEntities,
           bool mode, List<ProjectDto> Projects, IErrorLoggerService errorLoggerService)
        {

            var dtos = new List<GetAllCSFDto>();
            var i = 1;
            foreach (ServiceExplanationFinancial entity in serviceExplanationFinancialEntities)
            {
                try
                {
                    var dto = new GetAllCSFDto();
                    dto.Key = entity.Id;
                    dto.Row = i;
                    var releventSE = serviceExplanationEntities.Where(se => se.ID == entity.ServiceExplanationId).FirstOrDefault();
                    dto.ServiceExplanationId = entity.ServiceExplanationId;
                    dto.ServiceExplanationTitle = releventSE.Title;
                    //dto.ProjectId = releventSE.ProjectID;
                    //dto.ProjectName = releventSE.ProjectName;
                    //dto.ProposalId = releventSE.ProposalID;
                    //dto.ProposalName = releventSE.ProposalName;
                    dto.ProjectOrProposalName = "";
                    if (Projects != null)
                    {
                        if (Projects.Any())
                        {
                            if (releventSE.ActivityReference == AppCore.Enums.ActivityReferenceEnum.ProjectRefrence)
                            {
                                if (releventSE.ProjectID != null && releventSE.ProjectID != Guid.Empty)
                                {
                                    dto.ProjectOrProposalName = Projects.FirstOrDefault(x => x.Id == releventSE.ProjectID).Title;
                                }
                            }
                        }
                    }
                    
                    dto.ActivityCenterId = releventSE.ActivityCenterID;
                    dto.ActivityCenterTitle = releventSE.ActivityCenterTitle;
                    dto.CurrencyId = releventSE.CurrencyID;
                    dto.CurrencyTitle = releventSE.Currency.Title;
                    dto.UnitOfMeasurementId = releventSE.UnitOfMeasurementID;
                    dto.ProgramVolume = releventSE.ProgramVolume;
                    dto.DiscountAmount = releventSE.DiscountAmount;
                    dto.UnitAmount = releventSE.UnitAmount;
                    dto.TotalAmount = releventSE.TotalAmount;
                    dto.ExplanationStartingDate = releventSE.ExplanationStartingDate;
                    dto.ExplanationEndingDate = releventSE.ExplanationEndingDate;
                    //////To Be Developed
                    dto.VolumeSummation = string.Empty;
                    dto.PercentSummation = string.Empty;
                    //////To Be Developed
                    dto.ProjectCenterPercent = string.Empty;
                    dto.RequestedVolume = entity.RequestedVolume;
                    dto.RequestedPercent = entity.RequestedPercent;
                    dto.RequestedPrice = entity.RequestedPrice;
                    dto.ApprovedVolume = entity.ApprovedVolume;
                    dto.ApprovedPercent = entity.ApprovedPercent;
                    dto.ApprovedPrice = entity.ApprovedPrice;
                    dto.IsFinancial = true;
                    serviceExplanationEntities.Remove(releventSE);
                    dtos.Add(dto);
                    i++;
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
            }
            if (mode)
            {
                foreach (ServiceExplanation entity in serviceExplanationEntities)
                {
                    try
                    {
                        var dto = new GetAllCSFDto();
                        dto.Key = entity.ID;
                        dto.Row = i;
                        dto.ServiceExplanationId = entity.ID;
                        dto.ServiceExplanationTitle = entity.Title;
                        //dto.ProjectId = entity.ProjectID;
                        //dto.ProjectName = entity.ProjectName;
                        //dto.ProposalId = entity.ProposalID;
                        //dto.ProposalName = entity.ProposalName;
                        dto.ProjectOrProposalName = string.Empty;
                        dto.ProjectOrProposalName = "";
                        if (Projects != null)
                        {
                            if (Projects.Any())
                            {
                                if (entity.ActivityReference == AppCore.Enums.ActivityReferenceEnum.ProjectRefrence)
                                {
                                    if (entity.ProjectID != null && entity.ProjectID != Guid.Empty)
                                    {
                                        dto.ProjectOrProposalName = Projects.FirstOrDefault(x => x.Id == entity.ProjectID).Title;
                                    }
                                }
                            }
                        }
                        dto.ActivityCenterId = entity.ActivityCenterID;
                        dto.ActivityCenterTitle = entity.ActivityCenterTitle;
                        dto.CurrencyId = entity.CurrencyID;
                        dto.CurrencyTitle = entity.Currency.Title;
                        dto.UnitOfMeasurementId = entity.UnitOfMeasurementID;
                        dto.ProgramVolume = entity.ProgramVolume;
                        dto.DiscountAmount = entity.DiscountAmount;
                        dto.UnitAmount = entity.UnitAmount;
                        dto.TotalAmount = entity.TotalAmount;
                        dto.ExplanationStartingDate = entity.ExplanationStartingDate;
                        dto.ExplanationEndingDate = entity.ExplanationEndingDate;
                        dto.VolumeSummation = string.Empty;
                        dto.PercentSummation = string.Empty;
                        dto.ProjectCenterPercent = string.Empty;
                        dto.RequestedVolume = (entity.ProgramVolume != null) ? 0 : null;
                        dto.RequestedPercent = 0;
                        dto.RequestedPrice = 0;
                        dto.ApprovedVolume = (entity.ProgramVolume != null) ? 0 : null;
                        dto.ApprovedPercent = 0;
                        dto.ApprovedPrice = 0;
                        dto.IsFinancial = false;
                        dtos.Add(dto);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        errorLoggerService.SaveError(ex);
                        continue;
                    }
                }
            }
            return dtos;
        }
        /// <summary>
        /// زمانی از این مپر استفاده کم
        /// </summary>
        /// <param name="serviceExplanationEntities"></param>
        /// <param name="serviceExplanationFinancialEntities"></param>
        /// <param name="errorLoggerService"></param>
        /// <returns></returns>
        public static ServiceExplanationFinancial DtoToEntity(CUDSERequestedFinancialDto cUDSERequestedFinancialDto, CalculatedSERequestedFinancialDto calculatedSERequestedFinancialDto)
        {
            var entity = new ServiceExplanationFinancial();
            if (cUDSERequestedFinancialDto.Key == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            else
            {
                entity.Id = cUDSERequestedFinancialDto.Key;
            }
            entity.ServiceExplanationId = cUDSERequestedFinancialDto.ServiceExplanationId;
            entity.InvoiceBaseInformationId = cUDSERequestedFinancialDto.InvoiceBaseInformationId;
            entity.RequestedVolume = calculatedSERequestedFinancialDto.CalculatedRequestedVolume;
            entity.RequestedPercent = calculatedSERequestedFinancialDto.CalculatedRequestedPercent;
            entity.RequestedPrice = calculatedSERequestedFinancialDto.CalculatedRequestedPrice;
            entity.ApprovedVolume = (calculatedSERequestedFinancialDto.CalculatedRequestedVolume != null) ? 0 : null;
            entity.ApprovedPercent = 0;
            entity.ApprovedPrice = 0;
            entity.CurrencyId = cUDSERequestedFinancialDto?.CurrencyId ?? Guid.Empty;
            return entity;
        }
    }
}
