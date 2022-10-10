using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanConsolidatedDataFSDH : DataContractBase
    {
        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public double PrincipalOutstandingBal { get; set; }        

        [DataMember]
        public DateTime LastIntrPMTDate { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }
        
        [DataMember]
        public double AccruedIntr { get; set; }

        [DataMember]
        public double AmortizedCost{ get; set; }

         [DataMember]
        public double BookValue { get; set; }
          
        [DataMember]
         public double AmortDiff { get; set; }
        
    }
}
