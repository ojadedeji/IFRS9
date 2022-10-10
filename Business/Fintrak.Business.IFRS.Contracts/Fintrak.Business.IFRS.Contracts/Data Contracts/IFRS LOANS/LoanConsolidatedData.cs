using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanConsolidatedData : DataContractBase
    {
        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public DateTime Valuedate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public int Tenor { get; set; }

        [DataMember]
        public double Feeamount { get; set; }

        [DataMember]
        public double EarnedFee { get; set; }

        [DataMember]
        public double Unearnedfee { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public DateTime FirstRepaymentDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public double PrincipalRepayFreq { get; set; }

        [DataMember]
        public bool LD { get; set; }

        
    }
}
