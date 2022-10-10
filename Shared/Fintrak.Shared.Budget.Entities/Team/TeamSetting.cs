using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class TeamSetting : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TeamSettingId { get; set; }

        [DataMember]
        public bool EnableBudgetToMPRSynch{ get; set; }

        [DataMember]
        public bool EnableMPRToBudgetSynch { get; set; }

        public int EntityId
        {
            get
            {
                return TeamSettingId;
            }
        }
    }
}
