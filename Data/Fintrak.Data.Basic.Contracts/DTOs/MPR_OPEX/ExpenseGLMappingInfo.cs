using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class ExpenseGLMappingInfo
    {
        public ExpenseGLMapping ExpenseGLMapping { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }
        public GLDefinition GLDefinition { get; set; }

    }
}