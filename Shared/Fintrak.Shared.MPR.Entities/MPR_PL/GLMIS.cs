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
    public partial class GLMIS : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GlMisId { get; set; }

        [DataMember]
        [Required]
        public string GLAccount { get; set; }

          [DataMember]
        [Required]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string TeamCode { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionCode { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return GlMisId;
            }
        }
    }
}
