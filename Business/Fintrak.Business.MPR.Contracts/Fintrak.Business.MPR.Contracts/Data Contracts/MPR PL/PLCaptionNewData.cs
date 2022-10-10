using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class PLCaptionNewData : DataContractBase
    {
        [DataMember]
        public int PLCaptionId { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public AccountTypeEnum AccountType { get; set; }

        [DataMember]
        public string AccountTypeName { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public ModuleOwnerType ModuleOwnerType { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

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
