using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsLoanMissPaymentRepository : IDataRepository<IfrsLoanMissPayment>
    {
        //IEnumerable<IfrsLoanMissPayment> GetRecordByRefNo(string RefNo);
        //IEnumerable<IfrsLoanMissPayment> GetIfrsLoanMissPayments(int defaultCount, string path);
    }
}
