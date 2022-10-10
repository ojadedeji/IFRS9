using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ITestRepository : IDataRepository<Test>
    {
        IEnumerable<Test> GetTestBySearch(string searchParam, string path);
        IEnumerable<Test> GetTests(int defaultCount);
        IEnumerable<Test> ExportTest(int defaultCount, string path);
    }
}
