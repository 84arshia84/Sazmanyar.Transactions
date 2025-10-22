using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ContractFinancialDetailsRepository : IContractFinancialDetailsRepository
    {
        private readonly AppDbContext _context;
        public ContractFinancialDetailsRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// اضافه کردن مشخصات مالی قرارداد
        /// به دیتابیس
        /// </summary>
        /// <param name="contractFinancial"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> Add(ContractFinancialDetails contractFinancial)
        {
            try
            {
                var result=  _context.ContractFinancialDetails.AddAsync(contractFinancial);
                //_context.SaveChangesAsync();
                if(result.IsCompleted== true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// گرفتن مشخصات مالی یک قرارداد
        /// از دیتابیس
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns>ContractFinancialDetails</returns>
        /// <exception cref="ContractFinancialDetails"></exception>
        public async Task<ContractFinancialDetails> Get(Guid id)
        {
            try
            {
                var model = await _context.ContractFinancialDetails.Where(cf => cf.ID == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new ContractFinancialDetails();
            }
            catch (Exception ex)
            {

                return new ContractFinancialDetails();
            }
        }
        /// <summary>
        /// بروزرسانی مشخصات مالی قرارداد
        /// در دیتابیس
        /// </summary>
        /// <param name="contractFinancial"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> Update(ContractFinancialDetails contractFinancial)
        {
            try
            {
                var model= await Get(contractFinancial.ID);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(contractFinancial);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
