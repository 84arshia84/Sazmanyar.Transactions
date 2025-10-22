using AppCore.Entities.ContractsInformation.ContractLog;
using AppCore.Entities.ContractsInformation.Contratcs;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System.Text.Json;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    public static class ContractLogMapperProfile
    {
        public static ContractLog? MapContractLog(Contract oldContract, Contract newContract, string updatedBy, IErrorLoggerService logger, string logFilePath)
        {
            try
            {
                var changes = new List<ChangeDetailDto>();

                // اینجا تمام propertyهای مهم رو بررسی کن و اگر تغییری کرده ثبت کن
                if (oldContract.ContractTitle != newContract.ContractTitle)
                    changes.Add(new ChangeDetailDto
                    {
                        PropertyName = nameof(Contract.ContractTitle),
                        OldValue = oldContract.ContractTitle,
                        NewValue = newContract.ContractTitle
                    });

                if (oldContract.ContractFinancialDetails?.ContractValue_Added_Percent != newContract.ContractFinancialDetails?.ContractValue_Added_Percent)
                    changes.Add(new ChangeDetailDto
                    {
                        PropertyName = "ContractValue_Added_Percent",
                        OldValue = oldContract.ContractFinancialDetails?.ContractValue_Added_Percent.ToString(),
                        NewValue = newContract.ContractFinancialDetails?.ContractValue_Added_Percent.ToString()
                    });

                // سایر فیلدهای مهم را نیز به همین صورت اضافه کن...

                var log = new ContractLog
                {
                    Id = Guid.NewGuid(),
                    ContractId = newContract.ID,
                    ContractTitle = newContract.ContractTitle,
                    ContractNumber = newContract.ContractNumber,
                    UpdatedBy = updatedBy,
                    UpdatedAt = DateTime.Now,
                    ChangesSummary = JsonSerializer.Serialize(changes)
                };

                return log;
            }
            catch (Exception ex)
            {
                logger.SaveError(ex);
                return null;
            }
        }

        public static List<ContractLogDto> MapLogsToDtos(List<ContractLog> logs, IErrorLoggerService logger)
        {
            var result = new List<ContractLogDto>();
            foreach (var log in logs)
            {
                try
                {
                    var dto = new ContractLogDto
                    {
                        Id = log.Id,
                        ContractId = log.ContractId,
                        ContractTitle = log.ContractTitle,
                        ContractNumber = log.ContractNumber,
                        UpdatedBy = log.UpdatedBy,
                        UpdatedAt = log.UpdatedAt
                    };

                    if (!string.IsNullOrEmpty(log.ChangesSummary))
                    {
                        try
                        {
                            dto.Changes = JsonSerializer.Deserialize<List<ChangeDetailDto>>(log.ChangesSummary) ?? new List<ChangeDetailDto>();
                        }
                        catch
                        {
                            dto.Changes = new List<ChangeDetailDto>();
                        }
                    }

                    result.Add(dto);
                }
                catch (Exception ex)
                {
                    logger.SaveError(ex);
                }
            }

            return result;
        }
    }
}
