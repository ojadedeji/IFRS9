using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MPRSetupData : DataContractBase
    {
        [DataMember]
        public int SetupId { get; set; }

        [DataMember]
        public string ExcoDefinitionCode { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public string ExcoTeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public int AccountLenght { get; set; }

        [DataMember]
        public PoolOption PoolOption { get; set; }

        [DataMember]
        public string PoolName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string SwithMode { get; set; }

        [DataMember]
        public Nullable<int> LevelId { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
