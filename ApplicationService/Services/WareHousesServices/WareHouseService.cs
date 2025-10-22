using AppCore.Entities.User;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.WareHouseDtos;
using ApplicationService.Mapper.WareHouseEntitiesMappers;
using ApplicationService.Services.UserSerive;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WareHouses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApplicationService.Services.WareHousesServices
{
    internal class WareHouseService : IWareHouseService
    {
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        public WareHouseService(IErrorLoggerService errorLoggerService, IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CommodityDto>> GetAllCommodity(List<Guid> supplyListId)
        {
            try
            {
                string ids = "";
                for (int i = 0; i < supplyListId.Count; i++)
                {
                    ids += $"'{supplyListId[i].ToString()}'";
                    if (i< supplyListId.Count - 1)
                    {
                        ids += ",";
                    }
                }
                string connectionString = _configuration["ConnectionStrings:DbConnection"];

                string Query = $@"select SD.SupplyListID as SupplyListId ,sp.SupplyListName as SupplyListName, CommodityID as Id, C.CommodityName as Name,C.CommodityCode,
                                 SD.SupplyDate as SupplyDate,SD.RequiredAmount as theRequiredAmount, SD.FreeStock as FreeInventory, SD.ReservedStock as ReserveInventory,
                                 SD.OnTheWayStock as OnTheWayInventory, SD.Exited as ExitedInventory From WHM.SupplyListDetails SD 
                                 Inner Join WHM.Commodities C on C.ID = SD.CommodityID   inner join WHM.SupplyLists sp on sp.ID=SD.SupplyListID
                                 Where SupplyListID in ({ids}) and  SD.NeedToSupply > 0
                                ";
                return CommodityAutoMapperProfile.EntitiesToDtos(await _unitOfWork.WareHouseRepository.GetAllCommodity(supplyListId,Query, connectionString),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CommodityDto>();
            }
        }

        public async Task<List<SupplyListDto>> GetAllSupplyList()
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:DbConnection"];
                string pwaDb = _configuration["PWA:PwaDbName"];
                string pwaSchema = _configuration["PWA:PWASchemaName"];

                string Query = $@"SELECT distinct SL.ID AS Id ,SL.SupplyListName as Name  , SL.ProjectUID as ProjectId, P.ProjectTitle as ProjectName FROM WHM.SupplyLists AS SL
                                 INNER JOIN WHM.SupplyListDetails AS SLD ON SL.ID = SLD.SupplyListID 
                                 INNER JOIN {pwaDb}.{pwaSchema}.MSP_EpmProject_UserView P on P.ProjectUID = SL.ProjectUID
                                 WHERE SLD.NeedToSupply > 0 and SL.IsFinalApproved =1
                                ";
                return SupplyListAutoMapperProfile.EntitiesToDtos(await _unitOfWork.WareHouseRepository.GetAllSupplyList(Query, connectionString), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<SupplyListDto>();
            }
        }
    }
}
