using AppCore.Entities.PwaEntities.Projects;
using ApplicationService.DtoModels.PwaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Pwa
{
    public interface IProjectService
    {
        public Task<List<ProjectDto>> GetAll();
    }
}
