using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.ContractInformationRepository
{
    internal class ContractTimeProfileRepository : IContractTimeProfileRepository
    {
        private readonly AppDbContext _context;
        public ContractTimeProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// اضافه کردن مشخصات زمانی قرارداد 
        /// به دیتابیس
        /// </summary>
        /// <param name="contratTimeProfile"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> Add(ContractTimeProfile contratTimeProfile)
        {
            try
            {
                var result = _context.ContratTimeProfiles.AddAsync(contratTimeProfile);
                //_context.SaveChangesAsync();
                if(result.IsCompleted==true)
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
        /// گرفتن مشخصات زمانی یک قرارداد
        /// از دیتابیس
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns>ContractTimeProfile</returns>
        /// <exception cref="ContractTimeProfile"></exception>
        public async Task<ContractTimeProfile> Get(Guid id)
        {
            try
            {
                var model= _context.ContratTimeProfiles.Where(ct=>ct.ID==id).FirstOrDefault();
                if (model != null)
                {
                    return model;
                }
                return new ContractTimeProfile();
            }
            catch (Exception ex)
            {

                return new ContractTimeProfile();
            }
        }
        /// <summary>
        ///  بروزرسانی مشخصات زمانی قرارداد
        ///  در دیتابیس
        /// </summary>
        /// <param name="contratTimeProfile"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> Update(ContractTimeProfile contratTimeProfile)
        {
            try
            {
                var model = await Get(contratTimeProfile.ID);
                if (model != null)
                { 
                    _context.Entry(model).CurrentValues.SetValues(contratTimeProfile);
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
