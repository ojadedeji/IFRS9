using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class StaffCountData : DataContractBase
    {
        [DataMember]
        public int StaffCountId { get; set; }

        [DataMember]
        public string GradeCode { get; set; }

        [DataMember]
        public string GradeName { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string MisName { get; set; }
      
        [DataMember]
        public string CurrencyCode { get; set; }
                 
        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public string ClassificationCode { get; set; }

        [DataMember]
        public string ClassificationName { get; set; }

        [DataMember]
        public string DefintionCode { get; set; }

        [DataMember]
        public string DefintionName { get; set; }

        [DataMember]
        public TransactionTypeEnum TransactionType { get; set; }

        [DataMember]
        public CenterTypeEnum CenterType { get; set; }


        [DataMember]
        public string ReviewCode { get; set; }

        [DataMember]
        public string ReviewName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
