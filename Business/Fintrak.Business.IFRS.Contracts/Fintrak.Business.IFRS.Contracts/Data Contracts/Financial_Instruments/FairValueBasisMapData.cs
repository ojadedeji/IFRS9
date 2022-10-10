using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class FairValueBasisMapData : DataContractBase
    {
        [DataMember]
        public int FairValueBasisMapId { get; set; }

        [DataMember]
        public IFRSInstrument InstrumentType { get; set; }

        [DataMember]
        public string InstrumentTypeName { get; set; }

        [DataMember]
        public FIClassification Classification { get; set; }

        [DataMember]
        public string ClassificationName { get; set; }

        [DataMember]
        public int BasisLevel { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
