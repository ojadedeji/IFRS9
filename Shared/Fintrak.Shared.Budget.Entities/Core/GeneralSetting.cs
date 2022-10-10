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
    public partial class GeneralSetting : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GeneralSettingId { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        [Required]
        public Months StartMonth { get; set; }

        [DataMember]
        [Required]
        public Months EndMonth { get; set; }

        [DataMember]
        [Required]
        public Months CurrentMonth { get; set; }

        public int EntityId
        {
            get
            {
                return GeneralSettingId;
            }
        }
    }
}
