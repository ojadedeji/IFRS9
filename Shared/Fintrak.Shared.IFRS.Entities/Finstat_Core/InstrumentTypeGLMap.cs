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
    public partial class InstrumentTypeGLMap : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int InstrumentTypeGLMapId { get; set; }

        [DataMember]
        [Required]
        public int InstrumentTypeId { get; set; }

        [DataMember]
        [Required]
        public int GLTypeId { get; set; }

         [DataMember]
         [Required]
        public string GLCode { get; set; }

         [DataMember]
         [Required]
         public string CompanyCode { get; set; }

        
      
        public int EntityId
        {
            get
            {
                return InstrumentTypeGLMapId;
            }
        }
    }
}
