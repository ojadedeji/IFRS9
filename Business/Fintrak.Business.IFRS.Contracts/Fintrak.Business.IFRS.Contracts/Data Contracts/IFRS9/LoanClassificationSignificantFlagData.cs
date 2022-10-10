using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanClassificationSignificantFlagData : DataContractBase
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? LoanClassificationId { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string SICR_Flag { get; set; }

        [DataMember]
        public int SICR_ParamID { get; set; }
        [DataMember]
        public string SICRParameterName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
