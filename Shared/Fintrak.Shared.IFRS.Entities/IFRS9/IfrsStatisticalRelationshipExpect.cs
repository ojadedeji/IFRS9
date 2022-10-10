using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsStatisticalRelationshipExpect : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int sta_id { get; set; }

        [DataMember]
        public string mev_code { get; set; }

        [DataMember]
        public string mev_decs { get; set; }

        [DataMember]
        public string rel_exp { get; set; }

        public int EntityId
        {
            get
            {
                return sta_id;
            }
        }
    }
}
