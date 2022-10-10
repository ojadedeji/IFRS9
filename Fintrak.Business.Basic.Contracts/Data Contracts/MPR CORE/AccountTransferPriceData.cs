using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class AccountTransferPriceData : DataContractBase
    {
        [DataMember]
        public int AccountTransferPriceId { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public AccountTypeEnum Category { get; set; }


        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string Period { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
       
        [DataMember]
        public bool Active { get; set; }
    }
}

