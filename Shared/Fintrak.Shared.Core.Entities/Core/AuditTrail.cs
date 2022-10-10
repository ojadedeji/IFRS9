using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Shared.Core.Entities
{
    public partial class AuditTrail : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int AuditTrailId { get; set; }

        [DataMember]
        [Required]
        public DateTime RevisionStamp { get; set; }

        [DataMember]
        [Required]
        public string TableName { get; set; }

        [DataMember]
        [Required]
        public string UserName { get; set; }

        [DataMember]        
        public string IPAddress { get; set; }

        [DataMember]
        public AuditAction Actions { get; set; }

        [DataMember]
        public string ActionDescription { get; set; }

        [DataMember]
        public string OldData { get; set; }

        [DataMember]
        public string NewData { get; set; }

        [DataMember]
        public string ChangedColumns { get; set; }

        public int EntityId
        {
            get
            {
                return AuditTrailId;
            }
        }
    }
}
