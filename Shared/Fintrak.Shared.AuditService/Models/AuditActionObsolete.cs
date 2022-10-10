using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.AuditService
{
    public partial class AuditActionObsolete : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int id { get; set; }


        [DataMember]
        [Required]
        public string name { get; set; }

        [DataMember]
        [Required]
        public string description { get; set; }

        

        public int EntityId
        {
            get
            {
                return id;
            }
        }
    }
}
