using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class FiscalPeriodData : DataContractBase
    {
        [DataMember]
        public int FiscalPeriodId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public bool Closed { get; set; }

        [DataMember]
        public int FiscalYearId { get; set; }

        [DataMember]
        public string FiscalYearName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
