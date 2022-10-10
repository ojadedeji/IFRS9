using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Scorecard.Framework;

namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDKPIData : DataContractBase
    {
        [DataMember]
        public int KPIId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public PeriodType PeriodType { get; set; }

        [DataMember]
        public string PeriodTypeName { get; set; }

        [DataMember]
        public KPIDirection Direction { get; set; }

        [DataMember]
        public string DirectionName { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public bool IsKPICalculated { get; set; }

        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public bool IsTargetCalculated { get; set; }

        [DataMember]
        public string ScoreFormula { get; set; }

        [DataMember]
        public AggregateMethods AggregateMethod { get; set; }

        [DataMember]
        public string AggregateMethodName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
