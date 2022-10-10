using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class TeamData : DataContractBase
    {
        [DataMember]
        public int TeamId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public int TeamDefinitionId { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public bool CanClassified { get; set; }

        [DataMember]
        public bool CanUseStaffId { get; set; }


        [DataMember]
        public string StaffId { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public ModuleOwnerType ModuleOwnerType { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

    }
}
