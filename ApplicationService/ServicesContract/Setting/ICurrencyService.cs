using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ICurrencyService
    {
        public Task<(string message, bool isSuccss)> Add(CurrencyDto currency);
        public Task<(string message, bool isSuccss)> Update(CurrencyDto currency);
        public Task<(string message, bool isSuccss)> Delete(Guid currencyId);
        public Task<List<CurrencyDto>> GetAll();
        public Task<CurrencyDto> Get(Guid currencyId);
    }
}
