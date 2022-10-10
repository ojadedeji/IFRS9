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
    public partial class OBExposureCCF : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int obe_Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string OBE_Type { get; set; }

        [DataMember]
        public string Mapped_OBE_Type { get; set; }

        [DataMember]
        [Required]
        public DateTime ValueDate { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public Nullable<decimal> Rate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public bool Default_Flag { get; set; }   
              
        [DataMember]
        public DateTime Date_Of_Devolement { get; set; }

         [DataMember]
        public double Default_Amount_Crystallize { get; set; }
        
        [DataMember]
         public int? DOI { get; set; }

         [DataMember]
        public double YTM { get; set; }

         [DataMember]
         public double TimeAtInvocation_yr { get; set; }

        [DataMember]
        public int Flag { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public double OneYearPriorBal { get; set; }


        [DataMember]
        public double DefaultBal { get; set; }

        [DataMember]
        public double OneYearPriorUndrawn { get; set; }

        [DataMember]
        public double AdditionalDrawnWithinYr { get; set; }
        [DataMember]
        public bool Active { get; set; }        


        public int EntityId
        {
            get
            {
                return obe_Id;
            }
        }
    }
}
