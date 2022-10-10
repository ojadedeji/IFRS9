using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface IIndividualImpairmentRepository : IDataRepository<IndividualImpairment>
    {
        IEnumerable<IndividualImpairment> GetIndividualImpairments();
        IEnumerable<LoanDetails> GetIndividualImpairments(string refNo);
        IEnumerable<string> GetDistinctRefNos();

    }
}
