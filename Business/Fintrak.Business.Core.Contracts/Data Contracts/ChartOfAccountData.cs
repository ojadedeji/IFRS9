using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ChartOfAccountData : DataContractBase
    {
        [DataMember]
        public int ChartOfAccountId { get; set; }

        [DataMember]
        public AccountTypeEnum AccountType { get; set; }

        [DataMember]
        public string AccountTypeName { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string IfrsCaption { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int? FinancialTypeId { get; set; }

        [DataMember]
        public string FinancialTypeName { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string LongDescription { get; set; }
    }
}
