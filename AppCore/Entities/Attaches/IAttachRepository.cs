using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Attaches
{
    public interface IAttachRepository
    {
        public Task Add(Attach attach);
        public Task Add(List<Attach> attach);
        public Task Update(Attach attach);
        public Task<Attach> Get(Guid id);
        public Task<List<Attach>> GetAll(Guid sectionId);
        public Task Delete(Guid attach);
    }
}
