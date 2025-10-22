using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceLists;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.HowToPays;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using InfraStructure.EntityConfig.ContractConfig;
using InfraStructure.EntityConfig.SettingConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.Errors;
using AppCore.Entities.SettingEntities.PaymentMethods;
using AppCore.Entities.SettingEntities.Roles;
using AppCore.Entities.SettingEntities.StagesRoles;
using AppCore.Entities.SettingEntities.Stages;
using InfraStructure.EntityConfig.InvoiceConfig;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Entities.SettingEntities.DefaultCoefficients;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.SettingEntities.Locations;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.Attaches;
using InfraStructure.EntityConfig.AttachConfig;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.LookUpTables;
using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Entities.InvoiceInformations.Payments;
using InfraStructure.EntityConfig.DescriptionConfig;
using AppCore.Entities.Descriptions;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using InfraStructure.EntityConfig.InvoiceAccessGroupConfigs;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using InfraStructure.EntityConfig.ContractAccessGroupConfig;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.SettingEntities.Statuses;
using AppCore.Entities.SettingEntities.FactorTypes;
using InfraStructure.EntityConfig.FactorConfigs;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.Histories;
using AppCore.Entities.FactorAccessGroup;
using InfraStructure.EntityConfig.FactorAccessGroupConfig;
using InfraStructure.EntityConfig.HistoryConfig;
using AppCore.Entities.FactorInformation.FactorAmounts;
using InfraStructure.EntityConfig.DesignCodeConfigs;
using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes;
using AppCore.Entities.Organizations;
using InfraStructure.EntityConfig.OrganizationInformationConfig;
using InfraStructure.EntityConfig.AccountConfig;
using AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization;
using AppCore.Entities.ContractsInformation.ContractLog;

namespace InfrStructure.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //checklisttables
            modelBuilder.ApplyConfiguration(new CorespondTypeOfCoopRelConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new ContractTimeProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceExplanationConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new StagesRolesConfiguration());
            modelBuilder.Ignore<Stage>();
            modelBuilder.ApplyConfiguration(new InvoiceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceBaseInformationConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceExplanationFinancialConfiguration());
            modelBuilder.ApplyConfiguration(new ContractCoefficientConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultCoefficientsConfiguration());
            modelBuilder.ApplyConfiguration(new ContractEstimatedmeterConfiguration());
            modelBuilder.ApplyConfiguration(new ContractFinancialDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new AddendumTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractAddendumConfiguration());
            modelBuilder.ApplyConfiguration(new EstimatedMeterFinancialConfiguration());
            modelBuilder.ApplyConfiguration(new NettingProcessItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new CountyConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CorespondentLegalConfiguration());
            modelBuilder.ApplyConfiguration(new CorespondentRealConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfGuaranteeConfiguration());
            modelBuilder.ApplyConfiguration(new ForGuaranteeConfiguration());
            modelBuilder.ApplyConfiguration(new ReleaseConditionConfiguration());
            modelBuilder.ApplyConfiguration(new ContractGuaranteeConfiguration());
            modelBuilder.ApplyConfiguration(new AttachConfiguration());
            modelBuilder.ApplyConfiguration(new ContractTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CheckListConfiguration());
            modelBuilder.ApplyConfiguration(new LookUpTableConfiguration());
            modelBuilder.ApplyConfiguration(new LookUpTableInsideConfiguration());
            modelBuilder.ApplyConfiguration(new ContractCheckListValueConfiguration());
            modelBuilder.ApplyConfiguration(new RoleOfOrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupContractTypeConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupInvoiceTypeConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupOrganizationUnitConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupRoleOfOrganizationConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupGroupConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupPermissionsConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupUserConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupPropertiesConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAccessGroupConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupContractTypeConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupGroupConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupOrganizationUnitConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupPermissionsConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupPropertiesConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupRoleOfOrganizationConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupUserConfig());
            modelBuilder.ApplyConfiguration(new ContractAccessGroupSystemPartsConfig());
            modelBuilder.ApplyConfiguration(new TransactionExecutionRequestConfig());
            modelBuilder.ApplyConfiguration(new ExecutionRequestServiceExplanationsConfig());
            modelBuilder.ApplyConfiguration(new ExecutionRequestCheckListValueCconfig());
            modelBuilder.ApplyConfiguration(new PrePaymentDepreciationConfig());
            modelBuilder.ApplyConfiguration(new OnAccountDepreciationConfig());
            modelBuilder.ApplyConfiguration(new InvoiceAmountConfigiuration());
            modelBuilder.ApplyConfiguration(new StatusConfigiuration());
            modelBuilder.ApplyConfiguration(new FactorTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FactorConfiguration());
            modelBuilder.ApplyConfiguration(new FactorServiceExplanationConfiguration());
            modelBuilder.ApplyConfiguration(new FactorFinancialDetaileConfiguration());
            modelBuilder.ApplyConfiguration(new FactorTimeProfileConfiguration());
            modelBuilder.ApplyConfiguration(new FactorNettingProcessItemConfiguration());
            modelBuilder.ApplyConfiguration(new FactorPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupFactorTypeConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupGroupsConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupOrganizationUnitsConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupPermissionsConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupPropertiesConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupRoleOfOrganizationsConfig());
            modelBuilder.ApplyConfiguration(new FactorAccessGroupUsersConfig());
            modelBuilder.ApplyConfiguration(new HistoryConfiguration());
            modelBuilder.ApplyConfiguration(new FactorAmountCofiguration());
            modelBuilder.ApplyConfiguration(new ContractDesignCodeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractDesignCodeRelConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDesignCodeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDesignCodeRelConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionRequestDesignCodeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionRequestDesignCodeRelConfiguration());
            modelBuilder.ApplyConfiguration(new FactorDesignCodeConfiguration());
            modelBuilder.ApplyConfiguration(new FactorDesignCodeRelConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationInformationConfig());
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new InvoiceBaseInformationConfiguration());
            modelBuilder.ApplyConfiguration(new ContractLogConfiguration());


        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activitycenter> Activitycenters { get; set; }
        public DbSet<BasisForStartingTheProject> BasisForStartingTheProjects { get; set; }
        public DbSet<BasisFortheEndOftheProject> BasisFortheEndOftheProjects { get; set; }
        public DbSet<CorespondentLegal> CorespondentLegals { get; set; }
        public DbSet<CorespondentReal> CorespondentReals { get; set; }
        public DbSet<CreditSource> CreditSources { get; set; }
        public DbSet<FinePaymentMethod> FinePaymentMethods { get; set; }
        public DbSet<ForGuarantee> ForGuarantees { get; set; }
        public DbSet<HowToPay> HowToPay { get; set; }
        public DbSet<Organizationalunit> Organizationalunits { get; set; }
        public DbSet<ReasonForCancellation> ReasonForCancellations { get; set; }
        public DbSet<ReasonForTermination> ReasonForTerminations { get; set; }
        public DbSet<ReleaseCondition> ReleaseConditions { get; set; }
        public DbSet<TransActionType> TransActionTypes { get; set; }
        public DbSet<TypeOfCooperation> TypeOfCooperations { get; set; }
        public DbSet<CoresponedAndTypeCoopRels> CoresponedAndTypeCoopRels { get; set; }
        public DbSet<CorespondRealAndTypeOfCoopRel> CorespondRealAndTypeOfCoopRels { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTimeProfile> ContratTimeProfiles { get; set; }
        public DbSet<ContractFinancialDetails> ContractFinancialDetails { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListField> PriceListFields { get; set; }
        public DbSet<PriceListClause> PriceListClauses { get; set; }
        public DbSet<PriceListExplanation> PriceListExplanations { get; set; }
        public DbSet<ServiceExplanation> ServiceExplanations { get; set; }
        public DbSet<ErrorLogger> ErrorLogger { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StagesRoles> StagesRoles { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<InvoiceBaseInformation> InvoiceBaseInformations { get; set; }
        public DbSet<ContractEstimatedmeter> ContractEstimatedmeters { get; set; }
        public DbSet<ServiceExplanationFinancial> ServiceExplanationFinancial { get; set; }
        public DbSet<DefaultCoefficients> DefaultCoefficients { get; set; }
        public DbSet<ContractCoefficient> ContractCoefficients { get; set; }
        public DbSet<AddendumType> AddendumTypes { get; set; }
        public DbSet<ContractAddendum> ContractAddendums { get; set; }
        public DbSet<EstimatedMeterFinancial> EstimatedMeterFinancials { get; set; }
        public DbSet<NettingProcessItem> NettingProcessItems { get; set; }
        public DbSet<OrganizationInformation> OrganizationInformations { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<TypeOfGuarantee> TypeOfGuarantees { get; set; }
        public DbSet<ContractGuarantee> ContractGuarantees { get; set; }
        public DbSet<Attach> Attaches { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<LookUpTable> LookUpTables { get; set; }
        public DbSet<LookUpTableInside> LookUpTablesInside { get; set; }
        public DbSet<ContractCheckListValue> ContractCheckListValues { get; set; }
        public DbSet<RoleOfOrganization> RoleOfOrganizations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<InvoiceAccessGroup> InvoiceAccessGroups { get; set; }
        public DbSet<InvoiceAccessGroupUser> InvoiceAccessGroupUsers { get; set; }
        public DbSet<InvoiceAccessGroupGroup> InvoiceAccessGroupGroups { get; set; }
        public DbSet<InvoiceAccessGroupContractType> InvoiceAccessGroupContractTypes { get; set; }
        public DbSet<InvoiceAccessGroupInvoiceType> InvoiceAccessGroupInvoiceTypes { get; set; }
        public DbSet<InvoiceAccessGroupOrganizationUnit> InvoiceAccessGroupOrganizationUnits { get; set; }
        public DbSet<InvoiceAccessGroupPermissions> InvoiceAccessGroupPermissions { get; set; }
        public DbSet<InvoiceAccessGroupProperties> InvoiceAccessGroupProperties { get; set; }
        public DbSet<InvoiceAccessGroupRoleOfOrganization> InvoiceAccessGroupRoleOfOrganizations { get; set; }
        public DbSet<ContractAccessGroup> ContractAccessGroups { get; set; }
        public DbSet<ContractAccessGroupContractType> ContractAccessGroupContractTypes { get; set; }
        public DbSet<ContractAccessGroupGroups> ContractAccessGroupsGroups { get; set; }
        public DbSet<ContractAccessGroupOrganizationUnits> ContractAccessGroupOrganizationUnits { get; set; }
        public DbSet<ContractAccessGroupPermissions> ContractAccessGroupPermissions { get; set; }
        public DbSet<ContractAccessGroupProperties> ContractAccessGroupProperties { get; set; }
        public DbSet<ContractAccessGroupRoleOfOrganizations> ContractAccessGroupRoleOfOrganizations { get; set; }
        public DbSet<ContractAccessGroupUsers> ContractAccessGroupUsers { get; set; }
        public DbSet<ContractAccessGroupSystemParts> ContractAccessGroupSystemParts { get; set; }
        public DbSet<TransactionExecutionRequest> TransactionExecutionRequest { get; set; }
        public DbSet<ExecutionRequestCheckListValue> ExecutionRequestCheckListValue { get; set; }
        public DbSet<ExecutionRequestServiceExplanation> ExecutionRequestServiceExplanation { get; set; }
        public DbSet<PrePaymentDepreciation> PrePaymentDepreciations { get; set; }
        public DbSet<OnAccountDepreciation> OnAccountDepreciations { get; set; }
        public DbSet<InvoiceAmount> InvoiceAmount { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<FactorType> FactorTypes { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorTimeProfile> FactorTimeProfiles { get; set; }
        public DbSet<FactorFinancialDetaile> FactorFinancialDetailes { get; set; }
        public DbSet<FactorServiceExplanation> FactorServiceExplanations { get; set; }
        public DbSet<FactorNettingProcessItem> FactorNettingProcesses { get; set; }
        public DbSet<FactorPayment> FactorPayments { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<FactorAccessGroup> FactorAccessGroup { get; set; }
        public DbSet<FactorAccessGroupFactorType> FactorAccessGroupFactorType { get; set; }
        public DbSet<FactorAccessGroupGroups> FactorAccessGroupGroups { get; set; }
        public DbSet<FactorAccessGroupOrganizationUnits> FactorAccessGroupOrganizationUnits { get; set; }
        public DbSet<FactorAccessGroupPermissions> FactorAccessGroupPermissions { get; set; }
        public DbSet<FactorAccessGroupProperties> FactorAccessGroupProperties { get; set; }
        public DbSet<FactorAccessGroupRoleOfOrganizations> FactorAccessGroupRoleOfOrganizations { get; set; }
        public DbSet<FactorAccessGroupUsers> FactorAccessGroupUsers { get; set; }
        public DbSet<FactorAmount> FactorAmounts { get; set; }
        public DbSet<ContractDesignCode> ContractDesignCodes { get; set; }
        public DbSet<ContractDesignCodeParameterRel> ContractDesignCodeParameterRels { get; set; }
        public DbSet<FactorDesignCode> FactorDesignCodes { get; set; }
        public DbSet<FactorDesignCodeParameterRel> FactorDesignCodeParameterRels { get; set; }
        public DbSet<InvoiceDesignCode> InvoiceDesignCodes { get; set; }
        public DbSet<InvoiceDesignCodeParameterRel> InvoiceDesignCodeParameterRels { get; set; }
        public DbSet<TransActionExecutionDesignCode> TransActionExecutionDesignCodes { get;  set; }
        public DbSet<TransActionExecutionDesignCodeParameterRel> TransActionExecutionDesignCodeParameterRels { get; set; }
        public DbSet<InvoiceBaseCustomization> InvoiceBaseCustomizations { get; set; }

        public DbSet<ContractLog> ContractLogs { get; set; }



    }
}
