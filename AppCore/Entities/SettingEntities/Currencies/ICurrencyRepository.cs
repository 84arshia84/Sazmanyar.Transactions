using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Currencies
{
    public interface ICurrencyRepository
    {
        public Task<(string message,bool isSuccss)> Add(Currency currency);
        public Task<(string message, bool isSuccss)> Update(Currency currency);
        public Task<(string message, bool isSuccss)> Delete(Guid currencyId);
        public Task<List<Currency>> GetAll();
        public Task<Currency> Get(Guid currencyId);
    }
}
