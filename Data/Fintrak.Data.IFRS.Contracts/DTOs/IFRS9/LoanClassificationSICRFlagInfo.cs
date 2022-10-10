using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class loanClassSignFlagInfo
    {
        public LoanClassificationSICRSignFlag loanClassSignFlag { get; set; }
        public SICRParameters SICRParameter { get; set; }
       
    }
}