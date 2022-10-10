using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class MarkovMatrixData : DataContractBase
    {
        [DataMember]
        public int MarkovMatrixId { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string SectorName { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public double InitialPD { get; set; }
        [DataMember]
        public double InitialNonPD { get; set; }
        [DataMember]
        public double PDmatrix { get; set; }
        [DataMember]
        public double NPDmatrix { get; set; }
       

        [DataMember]
        public bool Active { get; set; }
    }
}
