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
    public class PLCaptionData : DataContractBase
    {
        [DataMember]
        public int PLCaptionId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AccountTypeEnum AccountType { get; set; }

        [DataMember]
        public string AccountTypeName { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public  string Color  { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public String ParentName { get; set; }

        [DataMember]
        public String CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
