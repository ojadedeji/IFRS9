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
    public partial class LoanInterestRate : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanInterestRate_Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime Date_PMT { get; set; }

        [DataMember]
        [Required]
        public DateTime FPD_Date { get; set; }

        [DataMember]
        [Required]
        public int Repayment_Freq { get; set; }
        
        [DataMember]
        [Required]
        public double Rate { get; set; }
        
        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return LoanInterestRate_Id;
            }
        }
    }
}
