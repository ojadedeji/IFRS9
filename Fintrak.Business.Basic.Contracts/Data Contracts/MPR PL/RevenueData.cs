using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class RevenueData : DataContractBase
    {

        [DataMember]
        public int RevenueId { get; set; }

        [DataMember]
        public string TransId { get; set; }

        [DataMember]
        public string TransDate { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string Indicator { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLAccount { get; set; }

        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }

        [DataMember]
        public string AccountTitle { get; set; }

        [DataMember]
        public decimal Amount_LCY { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}