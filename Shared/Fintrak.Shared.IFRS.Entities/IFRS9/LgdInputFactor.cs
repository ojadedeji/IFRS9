using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class LgdInputFactor : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LgdInputFactorId { get; set; }

        [DataMember]
        public string Business { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public double Beststimate { get; set; }

        [DataMember]
        public double Optimistic { get; set; }

        [DataMember]
        public double Downturn { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LgdInputFactorId;
            }
        }
    }
}
