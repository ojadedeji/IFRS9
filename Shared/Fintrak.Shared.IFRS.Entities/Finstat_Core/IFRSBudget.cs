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
    public partial class IFRSBudget : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int IFRSBudgetId { get; set; } 


        [DataMember]
        [Required]
        public string CaptionName { get; set; }

        [DataMember]
        [Required]
        public DateTime ReportDate { get; set; }

        [DataMember]
        [Required]
        public decimal StretchBudget { get; set; }

        [DataMember]
        [Required]
        public decimal BoardBudget { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
     

  

        public int EntityId
        {
            get
            {
                return IFRSBudgetId;
            }
        }
    }
}
