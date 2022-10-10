using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class CollateralRecov : DataContractBase
    {

        [DataMember]
        public double CollateralRecovAmt { get; set; }

        [DataMember]
        public double Haircut { get; set; }

    }
}
