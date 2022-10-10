using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDConfigurationData : DataContractBase
    {
        [DataMember]
        public int ConfigurationId { get; set; }

        [DataMember]
        public OperationMode Mode { get; set; }

        [DataMember]
        public PeriodType PeriodType { get; set; }

        [DataMember]
        public string TeamClassificationTypeCode { get; set; }

        [DataMember]
        public string TeamClassificationTypeName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
