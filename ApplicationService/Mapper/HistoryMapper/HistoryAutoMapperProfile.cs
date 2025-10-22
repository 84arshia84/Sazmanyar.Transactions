using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.Histories;
using ApplicationService.DtoModels.HistoryDto;

namespace ApplicationService.Mapper.HistoryMapper
{
    public static class HistoryAutoMapperProfile
    {
        public static List<HistoryDto> GetAll(this List<History> histories)
        {
            return histories.Select(history => new HistoryDto
            {
                ActionType = history.ActionType,
                OperationDate = history.OperationDate,
                UserName = history.UserName,

            }).ToList();
        }
    }
}
