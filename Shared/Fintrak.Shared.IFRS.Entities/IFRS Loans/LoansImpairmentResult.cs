using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class LoansImpairmentResult : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string Classification { get; set; }


        [DataMember]
        public string Performing { get; set; }

        [DataMember]
        public bool WatchListed { get; set; }

        [DataMember]
        public bool Significant { get; set; }


        [DataMember]
        public int AgedBaseOnLastCr { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double PrincipalOutstandingBal { get; set; }

        public double Interest_Receiv_Pay_UnEarn { get; set; }

        [DataMember]
   
        public double InterestInSuspense { get; set; }

        [DataMember]
        public double AmortizedBalance { get; set; }

        [DataMember]
        public double TotalAmortizedCost { get; set; }

        [DataMember]
        public string ImpairmentTrigger { get; set; }

        [DataMember]
        public string InitialSelection { get; set; }

        [DataMember]
        public int DaysToMaturity { get; set; }


        [DataMember]
        public int ExpiredDays { get; set; }

        [DataMember]
        public double PeriodicInterestRepayment { get; set; }

        [DataMember]
        public double PeriodicCFPerPrincRepayment { get; set; }


        [DataMember]
        public decimal RecoverableRate { get; set; }

        [DataMember]
        public double PMT { get; set; }


        [DataMember]
        public double RecoverableAmount { get; set; }

     

        [DataMember]
        public double CollateralRecoverableAmt { get; set; }

   

    
        [DataMember]
        public double TotalRecoverableAmount { get; set; }


        [DataMember]
        public double ImpairmentSwitchTest { get; set; }

        [DataMember]
        public string FinalSelection { get; set; }

        [DataMember]
        public double SpecificImpairment { get; set; }

        [DataMember]
        public double CollectiveImpairment { get; set; }

        [DataMember]
        public double TotalImpairment { get; set; }

        [DataMember]
        public double FairValueGain { get; set; }

        [DataMember]
        public double CollateralValue { get; set; }
        [DataMember]
        public double CollateralHairCut { get; set; }
        [DataMember]
        public string CollateralCategory { get; set; }

        [DataMember]
        public int NPER { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public double StaffFairValueAmount { get; set; }

        [DataMember]
        public double StaffFairValueGain { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public decimal EP { get; set; }

        [DataMember]
        public decimal LGD { get; set; }

        [DataMember]
        public decimal PD { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
