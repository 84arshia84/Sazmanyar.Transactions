using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.HowToPays
{
    public interface IHowToPayRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(HowToPay howToPay);
        public Task<HowToPay> Get(Guid id);
        public Task<List<HowToPay>> GetAll();
        public Task<Tuple<string, bool>> Update(HowToPay howToPay);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
