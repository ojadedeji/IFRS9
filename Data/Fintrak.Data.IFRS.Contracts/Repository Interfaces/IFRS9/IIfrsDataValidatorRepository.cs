using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsDataValidatorRepository : IDataRepository<IfrsDataValidator>
    {
        IEnumerable<IfrsDataValidator> GetRecordByRefNo(string RefNo);
        IEnumerable<IfrsDataValidator> GetIfrsDataValidators(int defaultCount, string path);
     
    }
}
