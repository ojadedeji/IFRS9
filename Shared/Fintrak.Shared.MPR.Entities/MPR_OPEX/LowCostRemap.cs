using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

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
    public partial class LowCostRemap : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int LowCostRemapId { get; set; }

        [DataMember]
        [Required]
        public string LowCostItem { get; set; }

        [DataMember]
        public string Remmaped { get; set; }

        [DataMember]
        public string FreqLevel { get; set; }

        public int EntityId
        {
            get
            {
                return LowCostRemapId;
            }
        }

    }
}
