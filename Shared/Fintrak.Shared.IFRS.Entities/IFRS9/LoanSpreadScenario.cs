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
    public partial class LoanSpreadScenario : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanSpreadScenarioId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }
        [DataMember]
     
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string Sector { get; set; }

        [DataMember]
        [Required]
        public string Rating { get; set; }

        [DataMember]
        public string Bucket { get; set; }

        [DataMember]
        public int Tenor { get; set; }

        [DataMember]
        public double LifeTimePD { get; set; }

        [DataMember]
        public double AnnualPD { get; set; }

        //[DataMember]
        //public double IRR { get; set; }

        [DataMember]
        public double Lgd { get; set; }

        [DataMember]
        public double EAD { get; set; }
        [DataMember]
        public double ImpairmentCharge { get; set; }
        
        [DataMember]
        public string SpreadBy { get; set; }

        //[DataMember]
        //public double BalanceOutstanding { get; set; }

         [DataMember]
        public double ExpectedLoss { get; set; }

         //[DataMember]
         //public double UndrawnAmount { get; set; }

        [DataMember]
         public double CashShortFall { get; set; }

        [DataMember]
        public double DiscountedValue { get; set; }
        
         [DataMember]
         public double PVofCollateral { get; set; }

         //[DataMember]
         //public string AssessmentType { get; set; }

        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public bool Significant { get; set; }
        public int EntityId
        {
            get
            {
                return LoanSpreadScenarioId;
            }
        }
    }
}
