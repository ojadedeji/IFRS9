using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanSetupData : DataContractBase
    {
        [DataMember]
        public int LoanSetupId { get; set; }

        [DataMember]
        public decimal SignificantLoanMarkUp { get; set; }

        [DataMember]
        public RiskRatingTypes RatingType { get; set; }

        [DataMember]
        public string RatingTypeName { get; set; }

        [DataMember]
        public bool EPOption { get; set; }

        [DataMember]
        public int EPDefault { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
