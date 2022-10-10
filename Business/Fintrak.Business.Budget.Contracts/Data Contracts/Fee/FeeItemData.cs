using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class FeeItemData : DataContractBase
    {
        [DataMember]
        public int FeeItemId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public string GroupName { get; set; }
      
        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string CalculationType { get; set; }

        [DataMember]
        public string Movement { get; set; }

        [DataMember]
        public FeeUnitEnum Unit { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public bool Budgetable { get; set; }

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
