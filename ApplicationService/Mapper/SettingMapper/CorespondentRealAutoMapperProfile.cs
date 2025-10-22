using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class CorespondentRealAutoMapperProfile
    {
        public static CorespondentReal DtoToEntity(CorespondentRealDto dto)
        {
            var entity = new CorespondentReal();
            entity.Name = dto.Name;
            entity.Family = dto.Family;
            entity.Address = dto.Address ==null? "": dto.Address;
            entity.ShabaNumber = dto.ShabaNumber;
            entity.BankAcountNumber = dto.BankAcountNumber;
            entity.BankName = dto.BankName;
            entity.BranchCodeAndName = dto.BranchCodeAndName;
            entity.IsDeleted = dto.IsDeleted;
            entity.IsReal = dto.IsReal;
            entity.NationalCode = dto.NationalCode;
            entity.ID = dto.key;
            entity.CorespondAndTypeOfCoopRels = dto.TypeOfCooperations != null ? DtoToEntityRel(dto.TypeOfCooperations,dto.key) : null;
            entity.CityId = dto.City;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.PostalCode = dto.PostalCode;
            entity.CountyId = dto.County;
            entity.ProvincId = dto.Province;
            entity.EconomicCode = dto.EconomicCode;
            entity.FatherName = dto.FatherName;
            entity.CertificateNumber = dto.CertificateNumber;
            return entity;
        }
        public static CorespondentRealDto EntityToDto(CorespondentReal entity, Int64 row)
        {
            var dto = new CorespondentRealDto();
            dto.Name = entity.Name;
            dto.Family = entity.Family;
            dto.FullName = entity.Name + " " + entity.Family;
            dto.Address = entity.Address;
            dto.ShabaNumber=entity.ShabaNumber;
            dto.BankAcountNumber=entity.BankAcountNumber;
            dto.BankName = entity.BankName; 
            dto.BranchCodeAndName = entity.BranchCodeAndName;
            dto.row = row;
            dto.key = entity.ID;
            dto.IsDeleted = entity.IsDeleted;
            dto.IsReal = entity.IsReal;
            dto.NationalCode = entity.NationalCode;
            dto.TypeOfCooperations = EntityToDtoRel(entity.CorespondAndTypeOfCoopRels);
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Email = entity.Email;
            dto.PostalCode = entity.PostalCode;
            dto.EconomicCode = entity.EconomicCode;
            dto.City = entity.CityId;
            dto.Province = entity.ProvincId;
            dto.County = entity.CountyId;
            dto.FatherName = entity.FatherName;
            dto.CertificateNumber = entity.CertificateNumber;
            return dto;
        }
        public static List<CorespondentReal> DtosToEntities(List<CorespondentRealDto> dtos)
        {
            var entities = new List<CorespondentReal>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new CorespondentReal();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<CorespondentRealDto> EntitiesToDtos(List<CorespondentReal> entities)
        {
            var dtos = new List<CorespondentRealDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new CorespondentRealDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
        public static List<CorespondRealAndTypeOfCoopRel> DtoToEntityRel(List<Guid> dtos, Guid coresponedID)
        {
            var entities = new List<CorespondRealAndTypeOfCoopRel>();
            foreach (var item in dtos)
            {
                var entity = new CorespondRealAndTypeOfCoopRel();
                entity.CorespondentRealID = coresponedID;
                entity.TypeOfCooperationID = item;
                entities.Add(entity);
            }
            return entities;
        }
        public static List<Guid> EntityToDtoRel(List<CorespondRealAndTypeOfCoopRel> entities)
        {
            var dtos = new List<Guid>();
            foreach (var item in entities)
            {
                var dto = item.TypeOfCooperationID;
                dtos.Add((Guid)dto);
            }
            return dtos;
        }
    }
}
