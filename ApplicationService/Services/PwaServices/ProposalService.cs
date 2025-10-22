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
    internal class ProposalService : IProposalService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        public ProposalService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<DeliverablePenDto>> GetAllDeliverablePens()
        {
            try
            {
                string PwaDbName = _configuration["PWA:ProposalDbName"];
                string Query = @$"
                       select d.ID as Id ,d.Title as DeliverableName,d.ResearchFormID as ProposalId
                       from {PwaDbName}.Proposal.DeliverableItem as d
                       order by d.Title";
                return ProposalAutoMapprProfile.EntitiesToDtosDeliverabelPen(
                     await _unitOfWork.ProposalRepository.GetAllDeliverablePens(Query, _configuration["ConnectionStrings:PwaDbConnection"])
                     );
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<DeliverablePenDto>();
            }

        }

        public async Task<List<ProposalDto>> GetAllProposals()
        {
            try
            {
                string PwaDbName = _configuration["PWA:ProposalDbName"];
                string Query = @$"
                        select p.ProposalID as Id ,p.ProjectName as ProposalName 
                        from  {PwaDbName}.Proposal.[Projects] as p
                        inner join proposal.ResearchForm as r on r.id=p.ProposalID order by p.ProjectName";
                return ProposalAutoMapprProfile.EntitiesToDtosProposal(
                    await _unitOfWork.ProposalRepository.GetAllProposals(Query, _configuration["ConnectionStrings:DbConnection"])
                    );
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ProposalDto>();
            }
        }
    }
}
