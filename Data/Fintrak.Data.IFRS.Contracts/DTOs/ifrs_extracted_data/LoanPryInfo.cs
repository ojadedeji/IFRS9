using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class LoanPryInfo
    {
        public LoanPry LoanPry { get; set; }
        public ScheduleType ScheduleType { get; set; }
     
    }
}