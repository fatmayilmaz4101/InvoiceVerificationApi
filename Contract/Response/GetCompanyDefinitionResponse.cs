using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    public record GetCompanyDefinitionResponse(List<CompanyDefinitionEntity> CompanyDefinitions);
};