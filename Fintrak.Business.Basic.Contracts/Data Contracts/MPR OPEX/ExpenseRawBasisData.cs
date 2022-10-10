using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class ExpenseRawBasisData : DataContractBase
    {
        [DataMember]
        public int ExpenseRawBasisId { get; set; }

        [DataMember]
        public string BasisCode { get; set; }

        [DataMember]
        public string BasisName { get; set; }

        [DataMember]
        public string MISCode { get; set; }
        [DataMember]
        public string MISName { get; set; }
        [DataMember]
        public double Weight { get; set; }
        
        [DataMember]
        public bool Active { get; set; }
    }
}
