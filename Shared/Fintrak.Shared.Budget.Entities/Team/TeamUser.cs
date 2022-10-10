using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class TeamUser : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TeamUserId { get; set; }

        [DataMember]
        [Required]
        public string LoginID { get; set; }

        [DataMember]
        public string PCDefinitionCode { get; set; }

        [DataMember]
        public string PCMisCode { get; set; }

        [DataMember]
        public string CCDefinitionCode { get; set; }

        [DataMember]
        public string CCMisCode { get; set; }

        [DataMember]
        public bool IsLock { get; set; }

        public int EntityId
        {
            get
            {
                return TeamUserId;
            }
        }
    }
}
