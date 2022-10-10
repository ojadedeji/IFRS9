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

namespace Fintrak.Shared.MPR.Entities
{
    public partial class MPRPLDerivedCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PLDerivedCaptionId { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        [Required]
        public string DependencyCaptionCode { get; set; }

         [DataMember]
        [Required]
        public double Factor { get; set; }

         [DataMember]
         [Required]
         public int Period { get; set; }

         [DataMember]
         [Required]
         public string Year { get; set; }

         [DataMember]
         public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return PLDerivedCaptionId;
            }
        }
    }
}
