using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Errors
{
    public interface IErrorLoggerRepository
    {
        public Task SaveError(ErrorLogger errorLogger);
    }
}
