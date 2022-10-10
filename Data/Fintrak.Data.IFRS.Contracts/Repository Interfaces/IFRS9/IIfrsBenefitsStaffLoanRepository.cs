using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsBenefitsStaffLoanRepository : IDataRepository<IfrsBenefitsStaffLoan>
    {
        IEnumerable<IfrsBenefitsStaffLoan> GetIfrsBenefitsStaffLoanBySearch(string searchParam, string path);
        IEnumerable<IfrsBenefitsStaffLoan> GetIfrsBenefitsStaffLoans(int defaultCount);
        IEnumerable<IfrsBenefitsStaffLoan> ExportIfrsBenefitsStaffLoan(int defaultCount, string path);
    }
}
