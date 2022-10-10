using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDCategoryData : DataContractBase
    {
        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
      
        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
