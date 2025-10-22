using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Descriptions
{
    public interface IDescriptionRepository
    {
        public Task Add(Description description);
        public Task Add(List<Description> description);
        public Task Update(Description description);
        public Task<Description> Get(Guid id);
        public Task<List<Description>> GetAll(Guid sectionId);
        public Task Delete(Guid description);
    }
}
