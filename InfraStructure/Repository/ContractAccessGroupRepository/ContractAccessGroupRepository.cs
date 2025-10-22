using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.InvoiceInformations.Payments;
using AppCore.Entities.User;
using AppCore.Enums;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InfraStructure.Repository.ContractAccessGroupRepository;

    internal class ContractAccessGroupRepository : IContractAccessGroupRepository
    {
        private readonly AppDbContext _context;

        #region Crud
        public ContractAccessGroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAccessGroup(ContractAccessGroup contractAccessGroup)
        {
            try
            {
                await _context.ContractAccessGroups.AddAsync(contractAccessGroup);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupContractTypes(List<ContractAccessGroupContractType> contractAccessGroupContractTypes)
        {
            try
            {
                await _context.ContractAccessGroupContractTypes.AddRangeAsync(contractAccessGroupContractTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupGroups(List<ContractAccessGroupGroups> contractAccessGroupGroups)
        {
            try
            {
                await _context.ContractAccessGroupsGroups.AddRangeAsync(contractAccessGroupGroups);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupOrganizationUnits(List<ContractAccessGroupOrganizationUnits> contractAccessGroupOrganizations)
        {
            try
            {
                await _context.ContractAccessGroupOrganizationUnits.AddRangeAsync(contractAccessGroupOrganizations);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupPermissions(ContractAccessGroupPermissions contractAccessGroupPermissions)
        {
            try
            {
                await _context.ContractAccessGroupPermissions.AddAsync(contractAccessGroupPermissions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupProperties(ContractAccessGroupProperties contractAccessGroupProperties)
        {
            try
            {
                await _context.ContractAccessGroupProperties.AddAsync(contractAccessGroupProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupRoleOfOrganizations(List<ContractAccessGroupRoleOfOrganizations> contractAccessGroupRoles)
        {
            try
            {
                await _context.ContractAccessGroupRoleOfOrganizations.AddRangeAsync(contractAccessGroupRoles);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupSystemPart(List<ContractAccessGroupSystemParts> contractAccessGroupSystemParts)
        {
            try
            {
                await _context.ContractAccessGroupSystemParts.AddRangeAsync(contractAccessGroupSystemParts);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddAccessGroupUsers(List<ContractAccessGroupUsers> contractAccessGroups)
        {
            try
            {
                await _context.ContractAccessGroupUsers.AddRangeAsync(contractAccessGroups);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteContractTypes(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupContractTypes.Where(x => x.ContractAccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// حذف کامل گروه دسترسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCore(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model == null) { return false; }
                await DeleteContractTypes(id);
                await DeleteGroups(id);
                await DeleteOrganizationUnits(id);
                await DeletePermmisions(id);
                await DeleteProperties(id);
                await DeleteRoles(id);
                await DeleteSystemParts(id);
                await DeleteUsers(id);
                _context.ContractAccessGroups.Remove(model);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteGroups(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupsGroups.Where(x => x.ParentGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteOrganizationUnits(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupOrganizationUnits.Where(x => x.ContractAccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeletePermmisions(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupPermissions.Where(x => x.ContractAccessGroupId == id).FirstOrDefaultAsync();
                _context.Remove(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteProperties(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupProperties.Where(x => x.ContractAccessGroupId == id).FirstOrDefaultAsync();
                _context.Remove(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteRoles(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupRoleOfOrganizations.Where(x => x.ContractAccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteSystemParts(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupSystemParts.Where(x => x.ContractAccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteUsers(Guid id)
        {
            try
            {
                var model = await _context.ContractAccessGroupUsers.Where(x => x.AccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ContractAccessGroup> Get(Guid id)
        {
            try
            {
                return await _context.ContractAccessGroups.AsNoTracking().AsSplitQuery()
                    .Include(x => x.ContractAccessGroupGroups)
                    .Include(x => x.ContractAccessGroupPermissions)
                    .Include(x => x.ContractAccessGroupGroupChildren)
                    .Include(x => x.ContractAccessGroupContractTypes)
                    .Include(x => x.ContractAccessGroupOrganizationUnits)
                    .Include(x => x.ContractAccessGroupProperties)
                    .Include(x => x.ContractAccessGroupRoleOfOrganizations)
                    .Include(x => x.ContractAccessGroupSystemParts)
                    .Include(x => x.ContractAccessGroupUsers).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ContractAccessGroup>> GetAll()
        {
            try
            {
                return await _context.ContractAccessGroups.OrderBy(x => x.Title).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Update(ContractAccessGroup contractAccessGroup)
        {
            try
            {
                var model = await Get(contractAccessGroup.Id);
                if (model != null)
                {
                    model.Title = contractAccessGroup.Title;
                    model.Description = contractAccessGroup.Description;
                    _context.ContractAccessGroups.Update(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region UserAccess
        public async Task<List<Guid>> GetContractAddendumsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds)
        {
            try
            {
                var contractIds = await GetContractsUserHaveAccess(contractTypeIds, orgUnitIds, roleOfOrgIds);
                if (contractIds != null)
                {
                    return await _context.ContractAddendums.Where(x => contractIds.Contains(x.ContractId) && x.IsDeleted == false).Select(x => x.Id).ToListAsync();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Guid>> GetContractsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds)
        {
            try
            {
                var contractIds = await _context.Contracts
                                 .Where(c => (contractTypeIds.Contains(c.ContractTypeID) &&
                                             orgUnitIds.Contains(c.OrganizationUnitID) &&
                                             roleOfOrgIds.Contains(c.RoleOFOrganizationID))
                                             && c.IsDeleted == false)
                                 .Select(c => c.ID)
                                 .ToListAsync();

                return contractIds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Guid>> GetTransactionsUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds)
        {
            try
            {
                var transactionsIds = await _context.TransactionExecutionRequest
                                    .Where(t => (contractTypeIds.Contains(t.ContractTypeId) &&
                                               orgUnitIds.Contains(t.OrganizationId))
                                               && t.IsDeleted == false)
                                    .Select(t => t.Id)
                                    .ToListAsync();
                return transactionsIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Guid>> GetContractTypeUserHaveAccess(List<Guid> accessGroupId)
        {
            try
            {
                var contractTypeIds = await _context.ContractAccessGroupContractTypes
                                     .Where(ct => accessGroupId.Contains(ct.ContractAccessGroupId))
                                     .Select(ct => ct.ContractTypeId)
                                     .ToListAsync();
                return contractTypeIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Guid>> GetOrganizationUserHaveAccess(List<Guid> accessGroupId)
        {
            try
            {
                var orgUnitIds = await _context.ContractAccessGroupOrganizationUnits
                                .Where(ou => accessGroupId.Contains(ou.ContractAccessGroupId))
                                .Select(ou => ou.OrganizationUnitId)
                                .ToListAsync();
                return orgUnitIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Guid>> GetRoleOfOrganizationUserHaveAccess(List<Guid> accessGroupId)
        {
            try
            {
                var roleOfOrgIds = await _context.ContractAccessGroupRoleOfOrganizations
                                  .Where(ro => accessGroupId.Contains(ro.ContractAccessGroupId))
                                  .Select(ro => ro.RoleOfOrganizationId)
                                  .ToListAsync();
                return roleOfOrgIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// دسترسی هایی که کاربر در سامانه دارد
        /// به همراه دسترسی ثبت
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(List<ContractAccessGroupPermissions> permissions, List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserPermissionsAndSaveAccess(Guid id)
        {
            try
            {
                var permitionQuery = @"
                       WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                        )
        
                               SELECT DISTINCT p.*
                               FROM TAM.ContractAccessGroupPermissions p
                               JOIN UserAccessGroups uag ON p.ContractAccessGroupId = uag.Id
                        ";
                var Permissions = await _context.ContractAccessGroupPermissions
                                        .FromSqlRaw(permitionQuery, new SqlParameter("@userId", id))
                                        .ToListAsync();

                var partsQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupSystemParts sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
                                  ";
                var SystemParts = await _context.ContractAccessGroupSystemParts
                                  .FromSqlRaw(partsQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeSave=1 or sp.OrganizationUnitSave=1 or sp.RoleOfOrganizationSave=1
                                  ";
                var Properties = await _context.ContractAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (Permissions, SystemParts, Properties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserViewAccess(Guid id)
        {
            try
            {
                var partsQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupSystemParts sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
                                  ";
                var SystemParts = await _context.ContractAccessGroupSystemParts
                                  .FromSqlRaw(partsQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeView = 1 or sp.OrganizationUnitView = 1 or sp.RoleOfOrganizationView = 1
                                  ";
                var Properties = await _context.ContractAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (SystemParts, Properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserEditAccess(Guid id)
        {
            try
            {
                var partsQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupSystemParts sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
                                  ";
                var SystemParts = await _context.ContractAccessGroupSystemParts
                                  .FromSqlRaw(partsQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeEdit = 1 or sp.OrganizationUnitEdit = 1 or sp.RoleOfOrganizationEdit = 1
                                  ";
                var Properties = await _context.ContractAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (SystemParts, Properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserSaveAccess(Guid id)
        {
            try
            {
                var partsQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupSystemParts sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
                                  ";
                var SystemParts = await _context.ContractAccessGroupSystemParts
                                  .FromSqlRaw(partsQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeSave = 1 or sp.OrganizationUnitSave = 1 or sp.RoleOfOrganizationSave = 1
                                  ";
                var Properties = await _context.ContractAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (SystemParts, Properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ContractAccessGroupUsers>> GetByUserId(Guid userId)
        {
            try
            {

                var query = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupUsers sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeDelete = 1 or sp.OrganizationUnitDelete = 1 or sp.RoleOfOrganizationDelete = 1
                                  ";
                var final = await _context.ContractAccessGroupUsers.FromSqlRaw(query, new SqlParameter("@userId", userId))
                                  .ToListAsync();
                return (final);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<(List<ContractAccessGroupSystemParts> systemParts, List<ContractAccessGroupProperties> properties)> GetUserDeleteAccess(Guid id)
        {
            try
            {
                var partsQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupSystemParts sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
                                  ";
                var SystemParts = await _context.ContractAccessGroupSystemParts
                                  .FromSqlRaw(partsQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.ContractAccessGroups ag
                               JOIN TAM.ContractAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.ContractAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.ContractAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.ContractAccessGroupId = uag.Id
	                           where sp.ContractTypeDelete = 1 or sp.OrganizationUnitDelete = 1 or sp.RoleOfOrganizationDelete = 1
                                  ";
                var Properties = await _context.ContractAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (SystemParts, Properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ContractAccessGroup>> GetAccessGroupsByUserId(Guid userId)
        {
            var sql = @"
                WITH UserGroups AS(
                    SELECT agu.AccessGroupId AS Id
                    FROM TAM.ContractAccessGroupUsers AS agu
                    WHERE agu.UserId = @userId


                    UNION ALL
                        

                   SELECT gg.ParentGroupId
                   FROM TAM.ContractAccessGroupGroups AS gg
                   INNER JOIN UserGroups ug ON ug.Id = gg.GroupId
               )
               SELECT DISTINCT cag.*
               FROM TAM.ContractAccessGroups AS cag
               INNER JOIN UserGroups ug ON ug.Id = cag.Id
               OPTION (MAXRECURSION 30);";

            var param = new SqlParameter("@userId", userId);

            return await _context.ContractAccessGroups
                .FromSqlRaw(sql, param)
                .AsNoTracking()
                .ToListAsync();
        }
    }





    #endregion