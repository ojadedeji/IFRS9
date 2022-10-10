using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class OpexReportInfo
    {
        public OpexReport OpexReport { get; set; }
        public GLDefinition GLDefinition { get; set; }
        public Branch Branch { get; set; }

    }
}