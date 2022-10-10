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
    public partial class IfrsAccessttcDownTurnResult : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public double TTCLGD { get; set; }

        [DataMember]
        public double DownTurnLGD { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
