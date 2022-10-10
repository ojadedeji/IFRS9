using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDKPIEntryData : DataContractBase
    {
        [DataMember]
        public int EntryId { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string MISName { get; set; }

        [DataMember]
        public string KPICode { get; set; }

        [DataMember]
        public string KPIName { get; set; }

        [DataMember]
        public decimal Actual { get; set; }

        [DataMember]
        public decimal Target { get; set; }

        [DataMember]
        public double Score { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
