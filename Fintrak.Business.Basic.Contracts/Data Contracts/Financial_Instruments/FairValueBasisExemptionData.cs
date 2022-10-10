using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class FairValueBasisExemptionData : DataContractBase
    {
        [DataMember]
        public int FairValueBasisExemptionId { get; set; }

        [DataMember]
        public IFRSInstrument InstrumentType { get; set; }

        [DataMember]
        public string InstrumentTypeName { get; set; }
      
        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public int BasisLevel { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
