using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class OpexReportData : DataContractBase
    {
        [DataMember]
        public int ReportId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}