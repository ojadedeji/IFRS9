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
    public partial class IFRSTbills : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int TbillId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public DateTime EffectiveDate { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }
 
        
        [DataMember]
        public double FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }
           
              
        [DataMember]
        public decimal InterestRate { get; set; }

         [DataMember]
        public decimal CurrentMarketYield { get; set; }
        
        [DataMember]
         public string Classification { get; set; }

         [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Flag { get; set; }

        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return TbillId;
            }
        }
    }
}
