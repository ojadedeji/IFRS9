using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class BalanceSheetThresholdData : DataContractBase
    {
        [DataMember]
        public int BalanceSheetThresholdId { get; set; }

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
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public double Rate { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
