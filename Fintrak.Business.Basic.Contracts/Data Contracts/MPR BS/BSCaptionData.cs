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
    public class BSCaptionData : DataContractBase
    {
        [DataMember]
        public int CaptionId { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public BalanceSheetType BalanceSheetType { get; set; }

        [DataMember]
        public string BalanceSheetTypeName { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string PLCaption { get; set; }

        [DataMember]
        public string PLCaptionName { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
