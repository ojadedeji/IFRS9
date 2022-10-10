using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class DatabaseData : DataContractBase
    {
        [DataMember]
        public int DatabaseId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string DatabaseName { get; set; }

        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string IntegratedSecurity { get; set; }

        [DataMember]
        public int? SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        [DataMember]
        public bool Active { get; set; }
    }
}
