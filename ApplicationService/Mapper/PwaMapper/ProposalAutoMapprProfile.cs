using AppCore.Entities.PwaEntities.Projects;
using AppCore.Entities.PwaEntities.Proposals;
using ApplicationService.DtoModels.PwaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PwaMapper
{
    internal static class ProposalAutoMapprProfile
    {
        public static Proposal DtoToEntityProposal(ProposalDto dto)
        {
            var entity = new Proposal();
            entity.ProposalName = dto.Title;
            entity.Id = dto.Id;
            return entity;
        }
        public static ProposalDto EntityToDtoProposal(Proposal entity)
        {
            var dto = new ProposalDto();
            dto.Title = entity.ProposalName;
            dto.Id = entity.Id;
            return dto;
        }
        public static List<Proposal> DtosToEntitiesProposal(List<ProposalDto> dtos)
        {
            var entities = new List<Proposal>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Proposal();
                entity = DtoToEntityProposal(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ProposalDto> EntitiesToDtosProposal(List<Proposal> entities)
        {
            var dtos = new List<ProposalDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ProposalDto();
                dto = EntityToDtoProposal(entities[i]);
                dtos.Add(dto);
            }
            return dtos;
        }

        public static DeliverablePen DtoToEntityDeliverabelPen(DeliverablePenDto dto)
        {
            var entity = new DeliverablePen();
            entity.DeliverableName = dto.Title;
            entity.Id = dto.Id;
            entity.ProposalId = dto.ProposalId;
            return entity;
        }
        public static DeliverablePenDto EntityToDtoDeliverabelPen(DeliverablePen entity)
        {
            var dto = new DeliverablePenDto();
            dto.Title = entity.DeliverableName;
            dto.Id = entity.Id;
            dto.ProposalId = entity.ProposalId;
            return dto;
        }
        public static List<DeliverablePen> DtosToEntitiesDeliverabelPen(List<DeliverablePenDto> dtos)
        {
            var entities = new List<DeliverablePen>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new DeliverablePen();
                entity = DtoToEntityDeliverabelPen(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<DeliverablePenDto> EntitiesToDtosDeliverabelPen(List<DeliverablePen> entities)
        {
            var dtos = new List<DeliverablePenDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new DeliverablePenDto();
                dto = EntityToDtoDeliverabelPen(entities[i]);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
