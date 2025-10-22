using AppCore.Entities.Errors;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationService.Services.ExceptionHandlingService
{
    public class ErrorLoggerService : IErrorLoggerService
    {
        private IUnitOfWork _unitOfWork;
        public ErrorLoggerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// سرویس ذخیره کردن خطاهاs
        /// </summary>
        /// <param name="error"></param>
        /// <param name="function"></param>
        public async Task SaveError(Exception error)
        {
            try
            {
                var errorModel = new ErrorLogger();
                errorModel.ID = Guid.NewGuid();
                errorModel.CreateTime = DateTime.Now;
                errorModel.Message = error.InnerException == null ? error.ToString() : error.InnerException.Message;
                errorModel.FunctionName = error.TargetSite.DeclaringType.FullName.ToString();
                errorModel.Line = error.StackTrace.Split(":")[2];
                await _unitOfWork.ErrorLoggerRepository.SaveError(errorModel);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorLogger();
                errorModel.ID = Guid.NewGuid();
                errorModel.CreateTime = DateTime.Now;
                errorModel.Message = error.InnerException == null ? error.ToString() : error.InnerException.Message;
                errorModel.FunctionName = error.TargetSite.DeclaringType.FullName.ToString();
                errorModel.Line = error.StackTrace;
                await _unitOfWork.ErrorLoggerRepository.SaveError(errorModel);
                await _unitOfWork.Save();
            }

        }
    }
}
