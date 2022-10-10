using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDConfigurationInfo
    {
        public SCDConfiguration SCDConfiguration  { get; set; }
        public Company Company { get; set; }
        public TeamClassificationType TeamClassificationType { get; set; }
    }
}

