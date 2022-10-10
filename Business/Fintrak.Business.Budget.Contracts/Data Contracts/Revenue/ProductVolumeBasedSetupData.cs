using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class ProductVolumeBasedSetupData : DataContractBase
    {
        [DataMember]
        public int ProductVolumeBasedSetupId { get; set; }

        [DataMember]
        public string MakeUpCode { get; set; }

        [DataMember]
        public string MakeUpName { get; set; }
      
        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }
     
        [DataMember]
        public string ReviewCode { get; set; }

        [DataMember]
        public string ReviewName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
