using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class ExpenseMappingData : DataContractBase
    {
        [DataMember]
        public int ExpenseMappingId { get; set; }

        [DataMember]
        public string BasisCode { get; set; }

        [DataMember]
        public string BasisName { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string ValueTypeName { get; set; }

        [DataMember]
        public string ItemTypeName { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public string ParentMisCode { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public double Weight { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
