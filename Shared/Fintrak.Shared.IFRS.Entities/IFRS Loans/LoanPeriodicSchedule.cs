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
    public partial class LoanPeriodicSchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public int Num_Pmt { get; set; }

        [DataMember]
        [Required]
        public DateTime Date_Pmt { get; set; }

        [DataMember]
        public double Amt_Prin_Init { get; set; }

        [DataMember]
        public double Amt_Pmt { get; set; }

        [DataMember]  
        public double Amt_Int_Pay { get; set; }

        [DataMember]
        public double Amt_Prin_Pay { get; set; }

  

        [DataMember]
        public double Amt_Prin_End { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

 

        [DataMember]
        public string AMRefNo { get; set; }

        [DataMember]
        public int AMnum_Pmt { get; set; }

        [DataMember]
        public DateTime AMDate_Pmt { get; set; }

        [DataMember]
        public double AMamt_Prin_Init { get; set; }

        [DataMember]
        public double AMAmt_Pmt { get; set; }

        [DataMember]
        public double AMAmt_Int_Pay { get; set; }

        [DataMember]
        public double AMAmt_Prin_Pay { get; set; }

        [DataMember]
        public double AMAmt_Prin_End { get; set; }

        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
