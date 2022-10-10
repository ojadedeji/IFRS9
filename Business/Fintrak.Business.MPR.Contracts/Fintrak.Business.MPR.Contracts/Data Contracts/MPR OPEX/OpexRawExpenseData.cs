using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class OpexRawExpenseData : DataContractBase
    {
        [DataMember]
        public int OpexRawExpenseId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CheckMisCode { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string TranID { get; set; }

        [DataMember]
        public string SubGLCode { get; set; }

        [DataMember]
        public double DR { get; set; }

        [DataMember]
        public double CR { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
