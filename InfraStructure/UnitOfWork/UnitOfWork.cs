using AppCore.Entities.Attaches;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.ContractLog;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.Descriptions;
using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes;
using AppCore.Entities.Errors;
using AppCore.Entities.FactorAccessGroup;
using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using AppCore.Entities.Histories;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.Payments;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.Organizations;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using AppCore.Entities.PwaEntities.Projects;
using AppCore.Entities.PwaEntities.Proposals;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using AppCore.Entities.SettingEntities.BasisFortheEndOftheProjects;
using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.DefaultCoefficients;
using AppCore.Entities.SettingEntities.FactorTypes;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.HowToPays;
using AppCore.Entities.SettingEntities.Locations;
using AppCore.Entities.SettingEntities.LookUpTables;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.PaymentMethods;
using AppCore.Entities.SettingEntities.ReasonForCancellations;
using AppCore.Entities.SettingEntities.ReasonForTerminations;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Entities.SettingEntities.Roles;
using AppCore.Entities.SettingEntities.Stages;
using AppCore.Entities.SettingEntities.StagesRoles;
using AppCore.Entities.SettingEntities.Statuses;
using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using AppCore.Entities.User;
using AppCore.Entities.WareHouseEntities;
using AppCore.UnitOfWork;
using InfraStructure.Repository.SettingRepository;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public IActivitycenterRepository ActivitycenterRepository { get;}
        public IBasisForStartingTheProjectRepository BasisForStartingTheProjectRepository {  get;}
        public IBasisFortheEndOftheProjectRepository BasisFortheEndOftheProjectRepository { get;}
        public ICorespondentLegalRepository CorespondentLegalRepository { get;}
        public ICorespondentRealRepository CorespondentRealRepository { get;}
        public ICreditSourceRepository CreditSourceRepository { get;}
        public IFinePaymentMethodRepository FinePaymentMethodRepository { get;}
        public IForGuaranteeRepository ForGuaranteeRepository { get;}
        public IHowToPayRepository HowToPayRepository { get;}
        public IOrganizationalunitRepository OrganizationalunitRepository { get;}
        public IReasonForCancellationRepository ReasonForCancellationRepository { get;}
        public IReasonForTerminationRepository ReasonForTerminationRepository { get;}
        public IReleaseConditionRepository ReleaseConditionRepository { get;}
        public ITransActionTypeRepository TransActionTypeRepository { get;}
        public ITypeOfCooperationRepository TypeOfCooperationRepository { get;}
        public IContractRepository ContractRepository { get;}
        public IContractFinancialDetailsRepository ContractFinancialDetailsRepository { get;}
        public IContractTimeProfileRepository ContractTimeProfileRepository { get;}
        public IServiceExplanationRepository ServiceExplanationRepository { get;}
        public IPriceListRepository PriceListRepository { get;}
        public IErrorLoggerRepository ErrorLoggerRepository { get;}
        public IPriceListExplanationRepository PriceListExplanationRepository { get;}
        public IPriceListFieldRepository PriceListFieldRepository { get;}
        public IPriceListClauseRepository PriceListClauseRepository { get;}
        public ICurrencyRepository CurrencyRepository { get;}
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public IUnitOfMeasurementRepository UnitOfMeasurementRepository { get;}
        public IStageRepository StageRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IStagesRolesRepository StagesRolesRepository { get; }
        public IInvoiceTypeRepository InvoiceTypeRepository { get; }
        public IInvoiceBaseInformationRepository InvoiceBaseInformationRepository { get; }
        public IContractEstimatedmeterRepository ContractEstimatedmeterRepository { get; }
        public IServiceExplanationFinancialRepository ServiceExplanationFinancialRepository { get; }
        public IContractCoefficientRepository ContractCoefficientRepository { get; }
        public IDefaultCoefficientsRepository DefaultCoefficientsRepository { get; }
        public IEstimatedMeterFinancialRepository EstimatedMeterFinancialRepository{ get; }
        public IAddendumTypeRepository AddendumTypeRepository { get; }
        public INettingProcessItemRepository NettingProcessItemRepository { get; }
        public IContractAddendumRepository ContractAddendumRepository { get; }
        public IUserRepository   UserRepository {  get; }
        public ILocationRepository LocationRepository { get; }
        public ITypeOfGuaranteeRepository TypeOfGuaranteeRepository { get; }
        public IContractGuaranteeRepository ContractGuaranteeRepository { get; }
        public IContractTypeRepository ContractTypeRepository { get; }
        public ICheckListRepository CheckListRepository { get; }
        public ILookUpTableRepository LookUpTableRepository { get; }
        public IRoleOfOrganizationRepository RoleOfOrganizationRepository { get; }
        public IContractCheckListValuesRepository ContractCheckListValuesRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IDescriptionRepository DescriptionRepository {  get; }
        public IAttachRepository AttachRepository { get; }
        public IInvoiceAccessGroupRepository  InvoiceAccessGroupRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IProposalRepository ProposalRepository {  get; }
        public IContractAccessGroupRepository ContractAccessGroupRepository { get; }
        public ITransactionExecutionRequestRepository TransactionExecutionRequestRepository { get; }
        public IExecutionRequestServiceExplanationRepository ExecutionRequestServiceExplanationRepository { get; }
        public IExecutionRequestCheckListValueRepository ExecutionRequestCheckListValueRepository { get; }
        public IPrePaymentDepreciationRepository PaymentDepreciationRepository {  get; }
        public IWareHouseRepository WareHouseRepository {  get; }
        public IOnAccountDepreciationRepository OnAccountDepreciationRepository { get; }
        public IInvoiceAmountRepository InvoiceAmountRepository { get; }
        public IStatusRepository StatusRepository { get; }
        public IFactorTypeRepository FactorTypeRepository { get; }
        public IFactorFinancialDetaileRepository FactorFinancialDetaileRepository { get; }
        public IFactorNettingProcessItemRepository FactorNettingProcessItemRepository { get; }
        public IFactorPaymentRepository FactorPaymentRepository { get; }
        public IFactorRepository FactorRepository { get; }
        public IFactorServiceExplanationRepository FactorServiceExplanationRepository { get; }
        public IFactorTimeProfileRepository FactorTimeProfileRepository { get; }
        public IHistoryRepository HistoryRepository {  get; }
        public IFactorAccessGroupRepository FactorAccessGroupRepository { get; }
        public IContractDesignCodeRepository ContractDesignCodeRepository { get; }
        public IFactorDesignCodeRepository FactorDesignCodeRepository { get; }
        public IInvoiceDesignCodeRepository InvoiceDesignCodeRepository { get; }
        public ITransActionExecutionDesignCodeRepository TransActionExecutionDesignCodeRepository { get; }

        public IOrganizationInformationRepository OrganizationInformationRepository { get; }
        public IContractLogRepository ContractLogRepository { get; }

        public UnitOfWork(AppDbContext db

             ,IActivitycenterRepository activitycenterRepository
             ,IBasisForStartingTheProjectRepository basisForStartingTheProjectRepository
             ,IBasisFortheEndOftheProjectRepository basisFortheEndOftheProjectRepository
             ,ICorespondentLegalRepository corespondentLegalRepository
             ,ICorespondentRealRepository corespondentRealRepository
             ,ICreditSourceRepository creditSourceRepository
             ,IFinePaymentMethodRepository finePaymentMethodRepository
             ,IForGuaranteeRepository forGuaranteeRepository
             ,IHowToPayRepository howToPayRepository 
             ,IOrganizationalunitRepository organizationalunitRepository
             ,IReasonForCancellationRepository reasonForCancellationRepository
             ,IReasonForTerminationRepository reasonForTerminationRepository
             ,IReleaseConditionRepository releaseConditionRepository
             ,ITransActionTypeRepository transActionTypeRepository
             ,ITypeOfCooperationRepository typeOfCooperationRepository
             ,IContractTimeProfileRepository contractTimeProfileRepository
             ,IContractRepository contractRepository
             ,IContractFinancialDetailsRepository contractFinancialDetailsRepository
             ,IServiceExplanationRepository serviceExplanationRepository
             ,IPriceListRepository priceListRepository
             ,IErrorLoggerRepository errorLoggerRepository
             ,IPriceListExplanationRepository priceListExplanationRepository
             ,IPriceListFieldRepository priceListFieldRepository
             ,IPriceListClauseRepository priceListClauseRepository
             ,ICurrencyRepository currencyRepository 
             ,IPaymentMethodRepository paymentMethodRepository
             ,IUnitOfMeasurementRepository unitOfMeasurementRepository
             ,IStageRepository stageRepository
             ,IRoleRepository roleRepository
             ,IStagesRolesRepository stagesRolesRepository
             ,IInvoiceTypeRepository invoiceTypeRepository
             ,IInvoiceBaseInformationRepository invoiceBaseInformationRepository
             ,IContractEstimatedmeterRepository contractEstimatedmeterRepository
             ,IServiceExplanationFinancialRepository serviceExplanationFinancialRepository
             ,IContractCoefficientRepository contractCoefficientRepository 
             ,IDefaultCoefficientsRepository defaultCoefficientsRepository
             ,IEstimatedMeterFinancialRepository estimatedMeterFinancialRepository
             ,IAddendumTypeRepository addendumTypeRepository
             ,INettingProcessItemRepository nettingProcessItemRepository
             ,IContractAddendumRepository contractAddendumRepository
             ,IUserRepository userRepository
             ,ILocationRepository locationRepository
             ,ITypeOfGuaranteeRepository typeOfGuaranteeRepository
             ,IContractGuaranteeRepository contractGuaranteeRepository
             ,IContractTypeRepository contractTypeRepository
             ,ICheckListRepository checkListRepository
             ,ILookUpTableRepository lookUpTableRepository
             ,IRoleOfOrganizationRepository roleOfOrganizationRepository
             ,IContractCheckListValuesRepository contractCheckListValuesRepository
             ,IPaymentRepository paymentRepository
             ,IDescriptionRepository descriptionRepository
             ,IAttachRepository attachRepository
             ,IInvoiceAccessGroupRepository invoiceAccessGroupRepository
             ,IProjectRepository projectRepository
             ,IProposalRepository proposalRepository
             ,IContractAccessGroupRepository contractAccessGroupRepository
             ,ITransactionExecutionRequestRepository transactionExecutionRequestRepository
             ,IExecutionRequestServiceExplanationRepository executionRequestServiceExplanationRepository
             ,IExecutionRequestCheckListValueRepository executionRequestCheckListValueRepository
             ,IPrePaymentDepreciationRepository prePaymentDepreciationRepository
             ,IWareHouseRepository wareHouseRepository
             ,IOnAccountDepreciationRepository onAccountDepreciationRepository
             ,IInvoiceAmountRepository invoiceAmountRepository
             ,IStatusRepository statusRepository
             ,IFactorTypeRepository factorTypeRepository
             ,IFactorFinancialDetaileRepository factorFinancialDetaileRepository
             ,IFactorNettingProcessItemRepository factorNettingProcessItemRepository
             ,IFactorPaymentRepository factorPaymentRepository
             ,IFactorRepository factorRepository
             ,IFactorServiceExplanationRepository factorServiceExplanationRepository
             ,IFactorTimeProfileRepository factorTimeProfileRepository
             ,IHistoryRepository historyRepository
             ,IFactorAccessGroupRepository factorAccessGroupRepository
             ,IContractDesignCodeRepository contractDesignCodeRepository
             ,IFactorDesignCodeRepository factorDesignCodeRepository 
             ,IInvoiceDesignCodeRepository invoiceDesignCodeRepository 
             ,ITransActionExecutionDesignCodeRepository transActionExecutionDesignCodeRepository
             ,IOrganizationInformationRepository organizationInformationRepository
             ,IContractLogRepository contractLogRepository

        )
        {
            _db=db;
            ActivitycenterRepository = activitycenterRepository;
            BasisForStartingTheProjectRepository = basisForStartingTheProjectRepository;
            BasisFortheEndOftheProjectRepository = basisFortheEndOftheProjectRepository;
            CorespondentLegalRepository = corespondentLegalRepository;
            CorespondentRealRepository = corespondentRealRepository;
            CreditSourceRepository = creditSourceRepository;
            FinePaymentMethodRepository = finePaymentMethodRepository;
            ForGuaranteeRepository = forGuaranteeRepository;
            HowToPayRepository = howToPayRepository;
            OrganizationalunitRepository = organizationalunitRepository;
            ReasonForCancellationRepository = reasonForCancellationRepository;
            ReasonForTerminationRepository = reasonForTerminationRepository;
            ReleaseConditionRepository = releaseConditionRepository; 
            TransActionTypeRepository = transActionTypeRepository;
            TypeOfCooperationRepository = typeOfCooperationRepository;
            ContractFinancialDetailsRepository = contractFinancialDetailsRepository;
            ContractRepository = contractRepository;
            ContractTimeProfileRepository = contractTimeProfileRepository;
            ServiceExplanationRepository = serviceExplanationRepository;
            PriceListRepository = priceListRepository;
            ErrorLoggerRepository = errorLoggerRepository;
            PriceListExplanationRepository = priceListExplanationRepository;
            PriceListFieldRepository = priceListFieldRepository;
            PriceListClauseRepository = priceListClauseRepository;
            CurrencyRepository = currencyRepository;
            PaymentMethodRepository = paymentMethodRepository;
            UnitOfMeasurementRepository = unitOfMeasurementRepository;
            StageRepository = stageRepository;
            RoleRepository = roleRepository;
            StagesRolesRepository = stagesRolesRepository;
            InvoiceTypeRepository = invoiceTypeRepository;
            InvoiceBaseInformationRepository = invoiceBaseInformationRepository;
            ContractEstimatedmeterRepository = contractEstimatedmeterRepository;
            ServiceExplanationFinancialRepository = serviceExplanationFinancialRepository;
            ContractCoefficientRepository = contractCoefficientRepository;
            DefaultCoefficientsRepository = defaultCoefficientsRepository;
            EstimatedMeterFinancialRepository = estimatedMeterFinancialRepository;
            AddendumTypeRepository = addendumTypeRepository;
            NettingProcessItemRepository = nettingProcessItemRepository;
            ContractAddendumRepository = contractAddendumRepository;
            UserRepository = userRepository;
            LocationRepository = locationRepository;
            TypeOfGuaranteeRepository = typeOfGuaranteeRepository;
            ContractGuaranteeRepository = contractGuaranteeRepository;
            ContractTypeRepository = contractTypeRepository;
            CheckListRepository = checkListRepository;
            LookUpTableRepository = lookUpTableRepository;
            RoleOfOrganizationRepository = roleOfOrganizationRepository;
            ContractCheckListValuesRepository = contractCheckListValuesRepository;
            PaymentRepository = paymentRepository;
            DescriptionRepository = descriptionRepository;
            AttachRepository = attachRepository;
            InvoiceAccessGroupRepository = invoiceAccessGroupRepository;
            ProjectRepository = projectRepository;
            ProposalRepository = proposalRepository;
            ContractAccessGroupRepository = contractAccessGroupRepository;
            TransactionExecutionRequestRepository = transactionExecutionRequestRepository;
            ExecutionRequestServiceExplanationRepository = executionRequestServiceExplanationRepository;
            ExecutionRequestCheckListValueRepository = executionRequestCheckListValueRepository;
            PaymentDepreciationRepository = prePaymentDepreciationRepository;
            WareHouseRepository = wareHouseRepository;
            OnAccountDepreciationRepository = onAccountDepreciationRepository;
            InvoiceAmountRepository = invoiceAmountRepository;
            StatusRepository = statusRepository;
            FactorTypeRepository = factorTypeRepository;
            FactorFinancialDetaileRepository = factorFinancialDetaileRepository;
            FactorPaymentRepository = factorPaymentRepository;
            FactorNettingProcessItemRepository = factorNettingProcessItemRepository;
            FactorRepository = factorRepository;
            FactorServiceExplanationRepository = factorServiceExplanationRepository;
            FactorTimeProfileRepository = factorTimeProfileRepository;
            HistoryRepository = historyRepository;
            FactorAccessGroupRepository = factorAccessGroupRepository;  
            ContractDesignCodeRepository = contractDesignCodeRepository;
            FactorDesignCodeRepository = factorDesignCodeRepository;
            InvoiceDesignCodeRepository = invoiceDesignCodeRepository;
            TransActionExecutionDesignCodeRepository = transActionExecutionDesignCodeRepository;
            OrganizationInformationRepository = organizationInformationRepository;
            ContractLogRepository = contractLogRepository;


        }
        public async Task<int> Save() => await _db.SaveChangesAsync();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
        }
    }
}
