using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.HistoryDto;
using ApplicationService.Mapper.HistoryMapper;
using ApplicationService.ServicesContract.History;

namespace ApplicationService.Services.HistoryService
{

    public class HistoryService : IHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddHistory(Guid userId, string action, Guid entityId, string userName)
        {
            try
            {
                await _unitOfWork.HistoryRepository.AddAsync(
                    new AppCore.Entities.Histories.History
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        ActionType = action,
                        UserName = userName,
                        EntityId = entityId,
                        OperationDate = DateTime.Now,
                        InsertDate = DateTime.Now,

                    });
                await _unitOfWork.Save();
                return (true);


            }
            catch (Exception ex)
            {

                return (false);

            }
        }


        public async Task<List<HistoryDto>> GetAll(Guid entityId)
        {
            try
            {
                var res = await _unitOfWork.HistoryRepository.GetAll(entityId);
                return res.GetAll();    

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

