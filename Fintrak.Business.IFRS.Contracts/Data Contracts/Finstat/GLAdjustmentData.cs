using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class GLAdjustmentData : DataContractBase
    {
        [DataMember]
        public int GLAdjustmentId { get; set; }

        [DataMember]
        public string AdjustmentCode { get; set; }

        [DataMember]
        public int GLId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public Indicator Indicator { get; set; }

        [DataMember]
        public string IndicatorName { get; set; }

        [DataMember]
        public AdjustmentType AdjustmentType { get; set; }

        [DataMember]
        public string AdjustmentTypeName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int CurrencyId { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Posted { get; set; }

       
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
