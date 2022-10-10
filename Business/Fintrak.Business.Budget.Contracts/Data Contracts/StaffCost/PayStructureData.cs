using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class PayStructureData : DataContractBase
    {
        [DataMember]
        public int PayStructureId { get; set; }

        [DataMember]
        public string GradeCode { get; set; }

        [DataMember]
        public string GradeName { get; set; }

        [DataMember]
        public string ClassificationCode { get; set; }

        [DataMember]
        public string ClassificationName { get; set; }
      
        [DataMember]
        public decimal GrossPay { get; set; }
                 
        [DataMember]
        public decimal ThirtheenMonth { get; set; }

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
