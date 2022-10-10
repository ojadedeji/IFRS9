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

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class CompanySecurity : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CompanySecurityId { get; set; }

        [DataMember]
        [Required]
        public string Root { get; set; }

        [DataMember]
        [Required]
        public string Filter { get; set; }

        [DataMember]
        [Required]
        public string Attributes { get; set; }

        [DataMember]
        [Required]
        public string Scope { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

            
        public int EntityId
        {
            get
            {
                return CompanySecurityId;
            }
        }

    }
}
