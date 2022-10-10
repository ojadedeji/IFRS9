using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class PrimaryLock : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PrimaryLockId { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public bool Lock { get; set; }

        [DataMember]
        public bool CanOverride { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return PrimaryLockId;
            }
        }
    }
}
