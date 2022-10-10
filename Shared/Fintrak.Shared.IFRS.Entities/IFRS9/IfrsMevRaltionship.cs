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
    public partial class IfrsMevRaltionship : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int rel_id { get; set; }

        [DataMember]
        public string mev { get; set; }

        [DataMember]
        public bool independent_variable { get; set; }

        [DataMember]
        public double corr_to_dependent_variable { get; set; }

        [DataMember]
        public string corr_relationship { get; set; }

        [DataMember]
        public double actual_relationship { get; set; }

        [DataMember]
        public string relationship_interpret { get; set; }

        [DataMember]
        public string remark { get; set; }

        public int EntityId
        {
            get
            {
                return rel_id;
            }
        }
    }
}
