using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Services.ContractInformationService
{
    public class ContractLogService : IContractLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _logFilePath;
        private readonly IErrorLoggerService _errorLoggerService;

        public ContractLogService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, string? logFilePath = null)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _logFilePath = logFilePath ?? Path.Combine(AppContext.BaseDirectory, "Logs", "ContractChanges.log");
        }

        public async Task AddContractLog(Contract oldContract, Contract newContract, string updatedBy)
        {
            try
            {
                var log = ContractLogMapperProfile.MapContractLog(oldContract, newContract, updatedBy, _errorLoggerService, _logFilePath);
                if (log != null)
                {
                    await _unitOfWork.ContractLogRepository.Add(log);
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
            }
        }

        public async Task<List<ContractLogDto>> GetLogsByContractId(Guid contractId)
        {
            try
            {
                var logs = await _unitOfWork.ContractLogRepository.GetByContractId(contractId);
                return ContractLogMapperProfile.MapLogsToDtos(logs, _errorLoggerService);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<ContractLogDto>();
            }
        }
    }
}
