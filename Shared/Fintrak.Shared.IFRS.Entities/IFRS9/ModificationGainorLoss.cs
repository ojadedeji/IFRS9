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
    public partial class ModificationGainorLoss: EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int  ID { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }

        [DataMember]
        [Required]
        public double amt_pmt { get; set; }


        [DataMember]
        [Required]
        public double amt_pmt_Adjusted { get; set; }

        [DataMember]
        [Required]
        public double FairValueGainLoss { get; set; }


       [DataMember]
       [Required]
       public DateTime date_pmt { get; set; }

        [DataMember]
        [Required]
        public double PerGainLoss { get; set; }
        

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
