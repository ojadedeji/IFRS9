using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDCategoryInfo
    {
        public SCDCategory SCDCategory { get; set; }
        public SCDCategory Parent { get; set; }
    }
}

