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
    public partial class MISReplacement : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int MISReplacementId { get; set; }

        [DataMember]
        [Required]
        public string OldMISCode { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string MISCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return MISReplacementId;
            }
        }
    }
}
