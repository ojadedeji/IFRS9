using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ExtractionData : DataContractBase
    {
        [DataMember]
        public int ExtractionId { get; set; }

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
        public string ProcedureName { get; set; }

        [DataMember]
        public string ScriptText { get; set; }

        [DataMember]
        public string NeedArchiveAction { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
