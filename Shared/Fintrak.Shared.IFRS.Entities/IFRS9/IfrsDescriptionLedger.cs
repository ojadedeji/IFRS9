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
    public partial class IfrsDescriptionLedger : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int id { get; set; }

        [DataMember]
        public string refno { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Ledger { get; set; }


        public int EntityId
        {
            get
            {
                return id;
            }
        }
    }
}
