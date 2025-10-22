using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.OrganizationInformationDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.OrganizationInformationMapperProfile
{
    public class OrganizationInformationMapperProfile
    {
        public static OrganizationInformation DtoToEntity(OrganizationInformationDto dto)
        {
            var entity = new OrganizationInformation();
            entity.CompanyName = dto.CompanyName;
            entity.Address = dto.Address == null ? "" : dto.Address;
            entity.RegistrationNumber = dto.RegistrationNumber;
            entity.ID = dto.Key;
            entity.NationalID = dto.NationalID;
            //entity.CorespondAndTypeOfCoopRels = dto.TypeOfCooperations != null ? DtoToEntityRel(dto.TypeOfCooperations, dto.key) : null;
            //entity.IsReal = dto.IsReal;
            entity.CityId = dto.City;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.PostalCode = dto.PostalCode;
            entity.CountyId = dto.County;
            entity.ProvincId = dto.Province;
            entity.Accounts = dto.AccountDtos?
                .Select(d => new Account
                {
                    Id = d.Id,
                    AccountNumber = d.AccountNumber,
                    Bank = d.Bank,
                    Branch = d.Branch,
                    IsActive = d.IsActive,
                    Sheba = d.Sheba,
                    OrganizationInformationId = d.OrganizationInformationId
                })
                .ToList();
            //entity.EconomicCode = dto.EconomicCode;
            return entity;
        }
        public static OrganizationInformationDto EntityToDto(OrganizationInformation entity)
        {
            var dto = new OrganizationInformationDto();
            dto.CompanyName = entity.CompanyName;
            dto.Address = entity.Address;
            dto.RegistrationNumber = entity.RegistrationNumber;
            dto.NationalID = entity.NationalID;
            dto.Key = entity.ID;
            //dto.IsReal = entity.IsReal;
            //dto.TypeOfCooperations = EntityToDtoRel(entity.CorespondAndTypeOfCoopRels);
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Email = entity.Email;
            dto.PostalCode = entity.PostalCode;
           // dto.EconomicCode = entity.EconomicCode;
            dto.City = entity.CityId;
            dto.Province = entity.ProvincId;
            dto.County = entity.CountyId;
            dto.AccountDtos = entity.Accounts?
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    AccountNumber = a.AccountNumber,
                    Bank = a.Bank,
                    Branch = a.Branch,
                    IsActive = a.IsActive,
                    Sheba = a.Sheba,
                    OrganizationInformationId = a.OrganizationInformationId
                })
                .ToList();
            return dto;
        }
        public static List<OrganizationInformation> DtosToEntities(List<OrganizationInformationDto> dtos)
        {
            var entities = new List<OrganizationInformation>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new OrganizationInformation();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<OrganizationInformationDto> EntitiesToDtos(List<OrganizationInformation> entities)
        {
            var dtos = new List<OrganizationInformationDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new OrganizationInformationDto();
                dto = EntityToDto(entities[i]);
                dtos.Add(dto);
            }
            return dtos;
        }
        //public static List<CoresponedAndTypeCoopRels> DtoToEntityRel(List<Guid> dtos, Guid coresponedID)
        //{
        //    var entities = new List<CoresponedAndTypeCoopRels>();
        //    foreach (var item in dtos)
        //    {
        //        var entity = new CoresponedAndTypeCoopRels();
        //        entity.ID = Guid.NewGuid();
        //        entity.CoresponedLegalID = coresponedID;
        //        entity.TypeOfCoopreationID = item;
        //        entities.Add(entity);
        //    }
        //    return entities;
        //}
        ////public static List<Guid> EntityToDtoRel(List<CoresponedAndTypeCoopRels> entities)
        ////{
        ////    var dtos = new List<Guid>();
        ////    foreach (var item in entities)
        ////    {
        ////        var dto = item.TypeOfCoopreationID;
        ////        dtos.Add((Guid)dto);
        ////    }
        ////    return dtos;
        ////}
    }
}

