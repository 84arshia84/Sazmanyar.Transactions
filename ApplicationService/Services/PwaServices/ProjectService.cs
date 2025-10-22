using AppCore.UnitOfWork;
using ApplicationService.DtoModels.PwaDtos;
using ApplicationService.Mapper.PwaMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Pwa;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.PwaServices
{
    internal class ProjectService : IProjectService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        public ProjectService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<ProjectDto>> GetAll()
        {
            try
            {

                string connectionString = _configuration["ConnectionStrings:PwaDbConnection"];
                string PwaDbName = _configuration["PWA:PwaDbName"];
                string PWASchemaName = _configuration["PWA:PWASchemaName"];
                string Query = @$"Select ProjectUID , ProjectName From {PwaDbName}.{PWASchemaName}.MSP_EpmProject_UserView   order by ProjectName ASC  ";
                return ProjectAutoMapprProfile.EntitiesToDtos(await _unitOfWork.ProjectRepository.GetAll(Query, connectionString));

            }
            catch (Exception ex)
            {
                return new List<ProjectDto>();
            }
        }
    }
}
