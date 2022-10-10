using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class MenuRoleData : DataContractBase
    {
        [DataMember]
        public int MenuRoleId { get; set; }

        [DataMember]
        public int MenuId { get; set; }

        [DataMember]
        public string MenuName { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
