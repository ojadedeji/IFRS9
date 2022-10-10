using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class TeamClassificationTypeData : DataContractBase
    {
        [DataMember]
        public int TeamClassificationTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int FiscalYearId { get; set; }

        [DataMember]
        public string FiscalYearName { get; set; }

        [DataMember]
        public int? CompanyId { get; set; }


        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
