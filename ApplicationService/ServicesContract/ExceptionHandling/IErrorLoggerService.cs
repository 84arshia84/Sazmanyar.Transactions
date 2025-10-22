using AppCore.Entities.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ExceptionHandling
{
    public interface IErrorLoggerService
    {
        public Task SaveError(Exception error);
    }
}
