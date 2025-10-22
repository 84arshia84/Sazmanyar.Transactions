using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using AppCore.Enums;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    public class InvoiceAccessGroupRepository : IInvoiceAccessGroupRepository
    {
        private readonly AppDbContext _context;
        public InvoiceAccessGroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(InvoiceAccessGroup invoiceAccessGroup, List<InvoiceAccessGroupInvoiceType> invoiceAccessGroupInvoiceTypes, List<InvoiceAccessGroupContractType> invoiceAccessGroupContractTypes
        , List<InvoiceAccessGroupOrganizationUnit> invoiceAccessGroupOrganizationUnits, List<InvoiceAccessGroupRoleOfOrganization> invoiceAccessGroupRoleOfOrganizations
        , List<InvoiceAccessGroupGroup> invoiceAccessGroupGroups, List<InvoiceAccessGroupUser> invoiceAccessGroupUsers, InvoiceAccessGroupPermissions invoiceAccessGroupPermissions
        , InvoiceAccessGroupProperties invoiceaAccessGroupProperties)
        {
            await _context.InvoiceAccessGroups.AddAsync(invoiceAccessGroup);
            if (invoiceAccessGroupInvoiceTypes != null)
            {
                await _context.InvoiceAccessGroupInvoiceTypes.AddRangeAsync(invoiceAccessGroupInvoiceTypes);
            }
            if (invoiceAccessGroupContractTypes != null)
            {
                await _context.InvoiceAccessGroupContractTypes.AddRangeAsync(invoiceAccessGroupContractTypes);
            }
            if (invoiceAccessGroupOrganizationUnits != null)
            {
                await _context.InvoiceAccessGroupOrganizationUnits.AddRangeAsync(invoiceAccessGroupOrganizationUnits);
            }
            if (invoiceAccessGroupRoleOfOrganizations != null)
            {
                await _context.InvoiceAccessGroupRoleOfOrganizations.AddRangeAsync(invoiceAccessGroupRoleOfOrganizations);
            }
            if (invoiceAccessGroupGroups != null)
            {
                await _context.InvoiceAccessGroupGroups.AddRangeAsync(invoiceAccessGroupGroups);
            }
            if (invoiceAccessGroupUsers != null)
            {
                await _context.InvoiceAccessGroupUsers.AddRangeAsync(invoiceAccessGroupUsers);
            }
            if (invoiceAccessGroupPermissions != null)
            {
                await _context.InvoiceAccessGroupPermissions.AddAsync(invoiceAccessGroupPermissions);
            }
            if (invoiceaAccessGroupProperties != null)
            {
                await _context.InvoiceAccessGroupProperties.AddAsync(invoiceaAccessGroupProperties);
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid Id)
        {
            try
            {
                var existingInvoiceAccessGroup = await _context.InvoiceAccessGroups.FindAsync(Id);
                if (existingInvoiceAccessGroup != null)
                {
                    await Task.Run(() => _context.InvoiceAccessGroups.Remove(existingInvoiceAccessGroup));
                    return ("حذف با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                return ("خطا در عملیات.", false);
            }
        }
        public async Task<InvoiceAccessGroup> GetById(Guid Id)
        {
            try
            {
                var accessGroup = await _context.InvoiceAccessGroups.AsNoTracking().AsSplitQuery()
                         .Include(a => a.InvoiceAccessGroupGroups)
                         .Include(a => a.InvoiceAccessGroupInvoiceTypes)
                         .Include(a => a.InvoiceAccessGroupPermissions)
                         .Include(a => a.InvoiceAccessGroupContractTypes)
                         .Include(a => a.InvoiceAccessGroupOrganizationUnits)
                         .Include(a => a.InvoiceAccessGroupProperties)
                         .Include(a => a.InvoiceAccessGroupRoleOfOrganizations)
                         .Include(a => a.InvoiceAccessGroupUsers)
                         .FirstOrDefaultAsync(a => a.Id == Id);
                return accessGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<InvoiceAccessGroup>> GetAll() => await _context.InvoiceAccessGroups.ToListAsync();
        public async Task<List<Guid>> GetInvoicesUserHaveAccess(List<Guid> contractTypeIds, List<Guid> orgUnitIds, List<Guid> roleOfOrgIds,List<Guid> invoiceTypeIds)
        {
            try
            {
                var invoiceIds = await _context.InvoiceBaseInformations
                                 .Where(i=> invoiceTypeIds.Contains(i.InvoiceTypeId) && i.IsDeleted==false)
                                 .Where(i => (contractTypeIds.Contains(i.Contract.ContractTypeID) &&
                                             orgUnitIds.Contains(i.Contract.OrganizationUnitID) &&
                                             roleOfOrgIds.Contains(i.Contract.RoleOFOrganizationID))
                                             && i.IsDeleted == false)
                                 .Select(c => c.Id)
                                 .ToListAsync();

                return invoiceIds;
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
                var contractTypeIds = await _context.InvoiceAccessGroupContractTypes
                                     .Where(ct => accessGroupId.Contains(ct.InvoiceAccessGroupId))
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
                var orgUnitIds = await _context.InvoiceAccessGroupOrganizationUnits
                                .Where(ou => accessGroupId.Contains(ou.InvoiceAccessGroupId))
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
                var roleOfOrgIds = await _context.InvoiceAccessGroupRoleOfOrganizations
                                  .Where(ro => accessGroupId.Contains(ro.InvoiceAccessGroupId))
                                  .Select(ro => ro.RoleOfOrganizationId)
                                  .ToListAsync();
                return roleOfOrgIds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Guid>> GetInvoiceTypeserHaveAccess(List<Guid> accessGroupId)
        {
            try
            {
                var roleOfOrgIds = await _context.InvoiceAccessGroupInvoiceTypes
                                  .Where(ro => accessGroupId.Contains(ro.InvoiceAccessGroupId))
                                  .Select(ro => ro.InvoiceTypeId)
                                  .ToListAsync();
                return roleOfOrgIds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceAccessGroupPermissions>> GetUserPermissions(Guid id)
        {
            try
            {
                var permitionQuery = @"
                       WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.InvoiceAccessGroups ag
                               JOIN TAM.InvoiceAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.InvoiceAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                        )
        
                               SELECT DISTINCT p.*
                               FROM TAM.InvoiceAccessGroupPermissions p
                               JOIN UserAccessGroups uag ON p.InvoiceAccessGroupId = uag.Id
                        ";
                var Permissions = await _context.InvoiceAccessGroupPermissions
                                        .FromSqlRaw(permitionQuery, new SqlParameter("@userId", id))
                                        .ToListAsync();

                return Permissions;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<InvoiceAccessGroupProperties>> GetUserViewAccess(Guid id)
        {
            try
            {

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.InvoiceAccessGroups ag
                               JOIN TAM.InvoiceAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.InvoiceAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.InvoiceAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.InvoiceAccessGroupId = uag.Id
	                           where sp.ContractTypeView = 1 or sp.OrganizationUnitView = 1 or sp.RoleOfOrganizationView = 1 or InvoiceTypeView = 1
                                  ";
                var Properties = await _context.InvoiceAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceAccessGroupProperties>> GetUserEditAccess(Guid id)
        {
            try
            {
                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.InvoiceAccessGroups ag
                               JOIN TAM.InvoiceAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.InvoiceAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.InvoiceAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.InvoiceAccessGroupId = uag.Id
	                           where sp.ContractTypeEdit = 1 or sp.OrganizationUnitEdit = 1 or sp.RoleOfOrganizationEdit = 1 or InvoiceTypeEdit = 1
                                  ";
                var Properties = await _context.InvoiceAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceAccessGroupProperties>> GetUserDeleteAccess(Guid id)
        {
            try
            {

                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.InvoiceAccessGroups ag
                               JOIN TAM.InvoiceAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.InvoiceAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.InvoiceAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.InvoiceAccessGroupId = uag.Id
	                           where sp.ContractTypeDelete = 1 or sp.OrganizationUnitDelete = 1 or sp.RoleOfOrganizationDelete = 1 or InvoiceTypeDelete = 1
                                  ";
                var Properties = await _context.InvoiceAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InvoiceAccessGroupProperties>> GetUserSaveAccess(Guid id)
        {
            try
            {
                var PropertiesQuery = @"
                         WITH UserAccessGroups AS (
                            -- Base case: groups the user is directly assigned to
                               SELECT ag.Id
                               FROM TAM.InvoiceAccessGroups ag
                               JOIN TAM.InvoiceAccessGroupUsers agu ON ag.Id = agu.AccessGroupId
                               WHERE agu.UserId = @userId
            
                               UNION ALL
            
                            -- Recursive case: parent groups of groups we've already found
                               SELECT parent.ParentGroupId
                               FROM TAM.InvoiceAccessGroupGroups parent
                               JOIN UserAccessGroups uag ON parent.GroupId = uag.Id
                          )
        
                               SELECT DISTINCT sp.*
                               FROM TAM.InvoiceAccessGroupProperties sp
                               JOIN UserAccessGroups uag ON sp.InvoiceAccessGroupId = uag.Id
	                           where sp.ContractTypeSave = 1 or sp.OrganizationUnitSave = 1 or sp.RoleOfOrganizationSave = 1 or InvoiceTypeSave = 1
                                  ";
                var Properties = await _context.InvoiceAccessGroupProperties
                                  .FromSqlRaw(PropertiesQuery, new SqlParameter("@userId", id))
                                  .ToListAsync();
                return Properties;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
