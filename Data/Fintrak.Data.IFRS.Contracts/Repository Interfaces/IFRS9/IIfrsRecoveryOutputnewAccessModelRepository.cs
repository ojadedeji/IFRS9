using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsRecoveryOutputnewAccessModelRepository : IDataRepository<IfrsRecoveryOutputnewAccessModel>
    {
        IEnumerable<IfrsRecoveryOutputnewAccessModel> GetIfrsRecoveryOutputnewAccessModelBySearch(string searchParam, string path);
        IEnumerable<IfrsRecoveryOutputnewAccessModel> GetIfrsRecoveryOutputnewAccessModels(int defaultCount);
        IEnumerable<IfrsRecoveryOutputnewAccessModel> ExportIfrsRecoveryOutputnewAccessModel(int defaultCount, string path);
    }
}
