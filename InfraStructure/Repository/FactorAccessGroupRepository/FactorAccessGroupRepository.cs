using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository.FactorAccessGroupRepository
{
    public class FactorAccessGroupRepository : IFactorAccessGroupRepository
    {
        private readonly AppDbContext _context;
        public FactorAccessGroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAccessGroup(FactorAccessGroup factorAccessGroup)
        {
            try
            {
                await _context.FactorAccessGroup.AddAsync(factorAccessGroup);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupFactorTypes(List<FactorAccessGroupFactorType> factorAccessGroupFactorTypes)
        {
            try
            {
                await _context.FactorAccessGroupFactorType.AddRangeAsync(factorAccessGroupFactorTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupGroups(List<FactorAccessGroupGroups> factorAccessGroupGroups)
        {
            try
            {
                await _context.FactorAccessGroupGroups.AddRangeAsync(factorAccessGroupGroups);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupOrganizationUnits(List<FactorAccessGroupOrganizationUnits> factorAccessGroupOrganizationUnits)
        {
            try
            {
                await _context.FactorAccessGroupOrganizationUnits.AddRangeAsync(factorAccessGroupOrganizationUnits);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupPermissions(FactorAccessGroupPermissions factorAccessGroupPermissions)
        {
            try
            {
                await _context.FactorAccessGroupPermissions.AddAsync(factorAccessGroupPermissions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupProperties(FactorAccessGroupProperties factorAccessGroupProperties)
        {
            try
            {
                await _context.FactorAccessGroupProperties.AddAsync(factorAccessGroupProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupRoleOfOrganizations(List<FactorAccessGroupRoleOfOrganizations> factorAccessGroupRoleOfOrganizations)
        {
            try
            {
                await _context.FactorAccessGroupRoleOfOrganizations.AddRangeAsync(factorAccessGroupRoleOfOrganizations);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAccessGroupUsers(List<FactorAccessGroupUsers> factorAccessGroupUsers)
        {
            try
            {
                await _context.FactorAccessGroupUsers.AddRangeAsync(factorAccessGroupUsers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCore(Guid id)
        {
            try
            {
                var model = await Get(id);
                if (model == null) { return false; }
                await DeleteFactorTypes(id);
                await DeleteGroups(id);
                await DeleteOrganizationUnits(id);
                await DeletePermmisions(id);
                await DeleteProperties(id);
                await DeleteRoles(id);
                await DeleteUsers(id);
                _context.FactorAccessGroup.Remove(model);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteFactorTypes(Guid id)
        {
            try
            {
                var model = await _context.FactorAccessGroupFactorType.Where(x => x.FactorAccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
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
                var model = await _context.FactorAccessGroupGroups.Where(x => x.ParentGroupId == id).ToListAsync();
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
                var model = await _context.FactorAccessGroupOrganizationUnits.Where(x => x.FactorAccessGroupId == id).ToListAsync();
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
                var model = await _context.FactorAccessGroupPermissions.Where(x => x.FactorAccessGroupId == id).FirstOrDefaultAsync();
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
                var model = await _context.FactorAccessGroupProperties.Where(x => x.FactorAccessGroupId == id).FirstOrDefaultAsync();
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
                var model = await _context.FactorAccessGroupRoleOfOrganizations.Where(x => x.FactorAccessGroupId == id).ToListAsync();
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
                var model = await _context.FactorAccessGroupUsers.Where(x => x.AccessGroupId == id).ToListAsync();
                _context.RemoveRange(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FactorAccessGroup> Get(Guid id)
        {
            try
            {
                return await _context.FactorAccessGroup.AsNoTracking().AsSplitQuery()
                    .Include(x => x.factorAccessGroupGroups)
                    .Include(x => x.factorAccessGroupPermissions)
                    .Include(x => x.FactorAccessGroupGroupChildren)
                    .Include(x => x.factorAccessGroupFactorTypes)
                    .Include(x => x.factorAccessGroupOrganizationUnits)
                    .Include(x => x.factorAccessGroupProperties)
                    .Include(x => x.factorAccessGroupRoleOfOrganizations)
                   
                    .Include(x => x.factorAccessGroupUsers).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FactorAccessGroup>> GetAll()
        {
            try
            {
                return await _context.FactorAccessGroup.OrderBy(x => x.Title).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Guid>> GetFactorTypeUserHaveAccess(List<Guid> accessGroupIds)
        {
            try
            {
                var contractTypeIds = await _context.FactorAccessGroupFactorType
                                     .Where(ct => accessGroupIds.Contains(ct.FactorAccessGroupId))
                                     .Select(ct => ct.FactorTypeId)
                                     .ToListAsync();
                return contractTypeIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Guid>> GetFactorUserHaveAccess(List<Guid> factorTypeIds, List<Guid> organizationUnitsIds, List<Guid> roleOfOrganizationsIds)
        {
            try
            {
                var factorIds = await _context.Factors.Where(c => (factorTypeIds.Contains((Guid)c.FactorTypeId)
                && organizationUnitsIds.Contains((Guid)c.OrganizationalunitId)
                && roleOfOrganizationsIds.Contains((Guid)c.RoleOfOrganizationId) && c.IsDeleted == false)).Select(c=>c.Id).ToListAsync();

                return factorIds;
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
                var orgUnitIds = await _context.FactorAccessGroupOrganizationUnits
                                .Where(ou => accessGroupId.Contains(ou.FactorAccessGroupId))
                                .Select(ou => ou.OrganizationUnitId)
                                .ToListAsync();
                return orgUnitIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Guid>> GetRoleOfOrganizationUserHaveAccess(List<Guid> accessGroupIds)
        {
            try
            {
                var roleOfOrgIds = await _context.FactorAccessGroupRoleOfOrganizations
                                  .Where(ro => accessGroupIds.Contains(ro.FactorAccessGroupId))
                                  .Select(ro => ro.RoleOfOrganizationId)
                                  .ToListAsync();
                return roleOfOrgIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorAccessGroupProperties>> GetUserDeleteAccess(Guid id)
        {
            try
            {

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.FactorAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.FactorAccessGroupId = uag.Id
	                           where sp.FactorTypeDelete = 1 or sp.OrganizationUnitDelete = 1 or sp.RoleOfOrganizationDelete = 1
                                  ";
                var Properties = await _context.FactorAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<List<FactorAccessGroupProperties>> GetUserEditAccess(Guid id)
        {
            try
            {
                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.FactorAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.FactorAccessGroupId = uag.Id
	                           where sp.FactorTypeEdit = 1 or sp.OrganizationUnitEdit = 1 or sp.RoleOfOrganizationEdit = 1
                                  ";
                var Properties = await _context.FactorAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FactorAccessGroupPermissions>> GetUserPermissions(Guid id)
        {
            try
            {
                var permitionQuery = @"
                       WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                        )
        
                               SELECT DISTINCT p.*
                               FROM TAM.FactorAccessGroupPermissions p
                               JOIN UserAccessGroups uag ON p.FactorAccessGroupId = uag.Id
                        ";
                var Permissions = await _context.FactorAccessGroupPermissions
                                        .FromSqlRaw(permitionQuery, new SqlParameter("@userId", id))
                                        .ToListAsync();

                return Permissions;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(List<FactorAccessGroupPermissions> permissions, List<FactorAccessGroupProperties> properties)> GetUserPermissionsAndSaveAccess(Guid id)
        {
            try
            {
                var permitionQuery = @"
                       WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                        )
        
                               SELECT DISTINCT p.*
                               FROM TAM.FactorAccessGroupPermissions p
                               JOIN UserAccessGroups uag ON p.FactorAccessGroupId = uag.Id
                        ";
                var Permissions = await _context.FactorAccessGroupPermissions
                                        .FromSqlRaw(permitionQuery, new SqlParameter("@userId", id))
                                        .ToListAsync();



                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.FactorAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.FactorAccessGroupId = uag.Id
	                           where  sp.FactorTypeSave = 1 or sp.OrganizationUnitSave = 1 or sp.RoleOfOrganizationSave = 1
                                  ";
                var Properties = await _context.FactorAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return (Permissions, Properties);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FactorAccessGroupProperties>> GetUserSaveAccess(Guid id)
        {
            try
            {
                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.FactorAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.FactorAccessGroupId = uag.Id
	                           where sp.FactorTypeSave = 1 or sp.OrganizationUnitSave = 1 or sp.RoleOfOrganizationSave = 1 
                                  ";
                var Properties = await _context.FactorAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public async Task<List<FactorAccessGroupProperties>> GetUserViewAccess(Guid id)
        {
            try
            {

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.FactorAccessGroup ag
                               JOIN TAM.FactorAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.FactorAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.FactorAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.FactorAccessGroupId = uag.Id
	                           where sp.FactorTypeView = 1 or sp.OrganizationUnitView = 1 or sp.RoleOfOrganizationView = 1
                                  ";
                var Properties = await _context.FactorAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(FactorAccessGroup factorAccessGroup)
        {
            try
            {
                var model = await Get(factorAccessGroup.Id);
                if (model != null)
                {
                    model.Title = factorAccessGroup.Title;
                    model.Description = factorAccessGroup.Description;
                    _context.FactorAccessGroup.Update(model);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
