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
    public partial class CCFModelling : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int CCFModellingId { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }
        [DataMember]
     
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string Grouping { get; set; }

        [DataMember]
        [Required]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime DefaultDate { get; set; }

        [DataMember]
        public double PrincipalOutstandingBal { get; set; }

        [DataMember]
        [Required]
        public double ODLimit { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public string Bucket { get; set; }

        [DataMember]
        public double BalatRefDate { get; set; }

        [DataMember]
        public double DefaultDatebal { get; set; }
        [DataMember]
        public double CCF { get; set; }
        
        [DataMember]
        [Required]
        public double DrawnAmt { get; set; }

        [DataMember]
        public double UnDrawnAmt { get; set; }

         [DataMember]
        public int TimeOfDefault { get; set; }

         [DataMember]
         public double UndrawnPercentage { get; set; }

         [DataMember]
         public double UtilizationFactor { get; set; }

       
        public int EntityId
        {
            get
            {
                return CCFModellingId;
            }
        }
    }
}
