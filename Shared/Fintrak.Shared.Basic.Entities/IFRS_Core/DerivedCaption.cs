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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class DerivedCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int DerivedCaptionId { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public string DependencyCaption { get; set; }

         [DataMember]
        [Required]
        public double Factor { get; set; }

         [DataMember]
         public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return DerivedCaptionId;
            }
        }
    }
}
