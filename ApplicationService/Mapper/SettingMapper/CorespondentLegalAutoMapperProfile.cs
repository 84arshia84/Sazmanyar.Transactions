using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class CorespondentLegalAutoMapperProfile
    {
        public static CorespondentLegal DtoToEntity(CorespondentLegalDto dto)
        {
            var entity = new CorespondentLegal();
            entity.CompanyName = dto.CompanyName;
            entity.ShabaNumber = dto.ShabaNumber;
            entity.Address = dto.Address == null ? "" : dto.Address;
            entity.RegistrationNumber = dto.RegistrationNumber;
            entity.BankAcountNumber = dto.BankAcountNumber;
            entity.BankName = dto.BankName;
            entity.BranchCodeAndName = dto.BranchCodeAndName;
            entity.ID = dto.key;
            entity.NationalID = dto.NationalID;
            entity.CorespondAndTypeOfCoopRels = dto.TypeOfCooperations != null? DtoToEntityRel(dto.TypeOfCooperations,dto.key):null;
            entity.IsDeleted = dto.IsDeleted;
            entity.IsReal = dto.IsReal;
            entity.CityId = dto.City;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.PostalCode = dto.PostalCode;
            entity.CountyId = dto.County;
            entity.ProvincId = dto.Province;
            entity.EconomicCode = dto.EconomicCode;
            return entity;
        }
        public static CorespondentLegalDto EntityToDto(CorespondentLegal entity, Int64 row)
        {
            var dto = new CorespondentLegalDto();
            dto.CompanyName = entity.CompanyName;
            dto.ShabaNumber = entity.ShabaNumber;
            dto.Address = entity.Address;
            dto.RegistrationNumber = entity.RegistrationNumber;
            dto.BankAcountNumber = entity.BankAcountNumber;
            dto.BankName = entity.BankName;
            dto.BranchCodeAndName= entity.BranchCodeAndName;
            dto.NationalID = entity.NationalID;
            dto.key = entity.ID;
            dto.row = row;
            dto.IsDeleted = entity.IsDeleted;
            dto.IsReal = entity.IsReal;
            dto.TypeOfCooperations= EntityToDtoRel(entity.CorespondAndTypeOfCoopRels);
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Email = entity.Email;
            dto.PostalCode = entity.PostalCode;
            dto.EconomicCode= entity.EconomicCode;
            dto.City = entity.CityId;
            dto.Province = entity.ProvincId;
            dto.County = entity.CountyId;
            return dto;
        }
        public static List<CorespondentLegal> DtosToEntities(List<CorespondentLegalDto> dtos)
        {
            var entities = new List<CorespondentLegal>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new CorespondentLegal();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<CorespondentLegalDto> EntitiesToDtos(List<CorespondentLegal> entities)
        {
            var dtos = new List<CorespondentLegalDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new CorespondentLegalDto();
                dto = EntityToDto(entities[i], i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
        public static List<CoresponedAndTypeCoopRels> DtoToEntityRel(List<Guid> dtos,Guid coresponedID)
        {
            var entities= new List<CoresponedAndTypeCoopRels>();
            foreach (var item in dtos)
            {
                var entity=new CoresponedAndTypeCoopRels();
                entity.ID = Guid.NewGuid();
                entity.CoresponedLegalID = coresponedID;
                entity.TypeOfCoopreationID = item;
                entities.Add(entity);
            }
            return entities;
        }
        public static List<Guid> EntityToDtoRel(List<CoresponedAndTypeCoopRels> entities)
        {
            var dtos = new List<Guid>();
            foreach (var item in entities)
            {
                var dto = item.TypeOfCoopreationID;
                dtos.Add((Guid)dto);
            }
            return dtos;
        }
    }
}
