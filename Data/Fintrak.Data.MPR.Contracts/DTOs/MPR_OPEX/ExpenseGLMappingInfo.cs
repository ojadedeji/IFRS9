using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ExpenseGLMappingInfo
    {
        public ExpenseGLMapping ExpenseGLMapping { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }
        public GLDefinition GLDefinition { get; set; }

    }
}