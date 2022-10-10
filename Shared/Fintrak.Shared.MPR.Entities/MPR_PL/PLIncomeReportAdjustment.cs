using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class PLIncomeReportAdjustment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }


        [DataMember]
        [Required]
        public string TeamCode { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [Required]
        public string Narrative { get; set; }


        [DataMember]
       
        public string BranchCode { get; set; }

        [DataMember]
      
        public string GLCode { get; set; }

    
        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }

     
        [DataMember]
        [Required]
        public decimal Amount { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }


        [DataMember]

        public string Code { get; set; }

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
