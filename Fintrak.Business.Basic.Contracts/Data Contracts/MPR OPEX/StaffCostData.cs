using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class StaffCostData : DataContractBase
    {
       
        [DataMember]
        public int StaffCostId { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string MISName { get; set; }
        
        [DataMember]
        public bool Active { get; set; }
    }
}
