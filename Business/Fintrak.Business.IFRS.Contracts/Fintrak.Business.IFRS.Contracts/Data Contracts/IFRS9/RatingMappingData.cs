using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class RatingMappingData : DataContractBase
    {
        [DataMember]
        public int RatingMappingId { get; set; }

        [DataMember]
        public int Credit_Risk_Id { get; set; }

        [DataMember]
        public string CreditRiskName { get; set; }

        [DataMember]
        public int External_Rating_Id { get; set; }

        [DataMember]
        public string ExternalRatingName { get; set; }
       

        [DataMember]
        public bool Active { get; set; }
    }
}
