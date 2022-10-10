using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsStaffBenefitsLoansRepository : IDataRepository<IfrsStaffBenefitsLoans>
    {
        IEnumerable<IfrsStaffBenefitsLoans> GetIfrsStaffBenefitsLoansBySearch(string searchParam, string path);
        IEnumerable<IfrsStaffBenefitsLoans> GetIfrsStaffBenefitsLoans(int defaultCount);
        IEnumerable<IfrsStaffBenefitsLoans> ExportIfrsStaffBenefitsLoans(int defaultCount, string path);
    }
}
