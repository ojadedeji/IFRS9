using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class IFRSProductInfo
    {
        public IFRSProduct IFRSProduct { get; set; }
        public Product Product { get; set; }
        public ScheduleType ScheduleType { get; set; }
    }
}