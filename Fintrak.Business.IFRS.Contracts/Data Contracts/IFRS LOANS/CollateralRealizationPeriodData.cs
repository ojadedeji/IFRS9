using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class CollateralRealizationPeriodData : DataContractBase
    {            
        [DataMember]
        public int CollateralRealizationPeriodId { get; set; }

        [DataMember]
        public string TypeCode{ get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
