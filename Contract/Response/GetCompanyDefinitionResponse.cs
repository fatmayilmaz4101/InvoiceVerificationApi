using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    public record GetCompanyDefinitionResponse(List<CompanyDefinitionEntity> CompanyDefinitions);
};