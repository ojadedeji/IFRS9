using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class UploadData : DataContractBase
    {
        [DataMember]
        public int UploadId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public string TruncateAction { get; set; }

        [DataMember]
        public string PostUploadAction { get; set; }

        [DataMember]
        public string Verification { get; set; }
        
        [DataMember]
        public string Template { get; set; }

        [DataMember]
        public Boolean BulkUpload { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
