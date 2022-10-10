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
    public partial class UnquotedEquityMarketabilityDiscount : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public double Base { get; set; }

        [DataMember]
        public double Best { get; set; }

        [DataMember]
        public double Worst { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
