using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class PostingDetailData : DataContractBase
    {
        [DataMember]
        public int PostingDetailId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public string TransDescription { get; set; }

        [DataMember]
        public string Indicator { get; set; }

         [DataMember]
        public decimal GAAPAmount { get; set; }

        [DataMember]
        public decimal ComputedAmount { get; set; }

        [DataMember]
        public decimal IFRSAmount { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string TransactionId { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public int ReportType { get; set; }

        [DataMember]
        public string ReportTypeName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
