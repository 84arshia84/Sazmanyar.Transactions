using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PwaEntities.Projects
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll(string pwaDb,string connectionString);
    }
}
