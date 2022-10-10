using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class TransferPriceData : DataContractBase
    {
        [DataMember]
        public int TransferPriceId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string MisName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string Period { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        [DataMember]
        public bool Active { get; set; }
    }
}
