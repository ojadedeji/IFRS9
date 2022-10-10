using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDKPIInfo
    {
        public SCDKPI SCDKPI { get; set; }
        public SCDCategory SCDCategory { get; set; }

    }
}

