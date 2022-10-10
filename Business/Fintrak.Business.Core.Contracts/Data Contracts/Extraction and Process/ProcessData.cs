using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ProcessData : DataContractBase
    {
        [DataMember]
        public int ProcessId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public PackageRunType RunType { get; set; }

        [DataMember]
        public string RunTypeName { get; set; }

        [DataMember]
        public string PackageName { get; set; }

        [DataMember]
        public string PackagePath { get; set; }
        
        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}
