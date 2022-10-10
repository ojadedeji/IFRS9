using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class GLAdjustmentData : DataContractBase
    {
        [DataMember]
        public int GLAdjustmentId { get; set; }

        [DataMember]
        public int GLId { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public Indicator Indicator { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public int CompanyId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public int CurrencyId { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public bool Posted { get; set; }

        [DataMember]
        public AdjustmentType AdjustmentType { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
