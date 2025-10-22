using ApplicationService.DtoModels.InvoiceDtos.InvoiceTypeDtos;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.ServicesContract.Users;
using Microsoft.Extensions.Configuration;
using AppCore.Enums;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class InvoiceTypeService : IInvoiceTypeService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IErrorLoggerService _errorLoggerService;
        private IInvoiceAccessGroupFilterService _filterService;
        private IUserService _userService;
        private IConfiguration _configuration;
        public InvoiceTypeService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IInvoiceAccessGroupFilterService filterService, IUserService userService,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _filterService = filterService;
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<List<InvoiceTypeDto>> GetAll()
        {
            try
            {
                var rawResult = await _unitOfWork.InvoiceTypeRepository.GetAll();
                var mappedResult = InvoiceTypeMapper.EntitiesToDtos(rawResult, _errorLoggerService);
                return mappedResult;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceTypeDto>();
            }
        }
        public async Task<List<InvoiceTypeDto>> GetAllInvoiceTypesWithAccessGroupEffect(string fullqualifyname, int mode)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(fullqualifyname, _configuration["ConnectionStrings:DbConnection"]);
                var rawResult = await _unitOfWork.InvoiceTypeRepository.GetAll();

                rawResult = await _filterService.FilterInvoiceType(rawResult, user.ID, SystemParts.Invoice, mode == 1 ? AccessGroupProperties.SaveInvoiceType : AccessGroupProperties.ViewInvoiceType, mode);
                var mappedResult = InvoiceTypeMapper.EntitiesToDtos(rawResult, _errorLoggerService);
                return mappedResult;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<InvoiceTypeDto>();
            }
        }

        public async Task<bool> Update(Guid id, Guid officeOnlineId)
        {
            try
            {
                var models = await _unitOfWork.InvoiceTypeRepository.GetAll();
                var model = models.FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    model.OfficeOnlineDocumentId = officeOnlineId;
                    await _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
