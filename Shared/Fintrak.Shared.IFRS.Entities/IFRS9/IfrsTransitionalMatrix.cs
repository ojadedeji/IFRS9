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
    public partial class IfrsTransitionalMatrix : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public double a { get; set; }

        [DataMember]
        public double b { get; set; }

        [DataMember]
        public int Rowno { get; set; }

        [DataMember]
        public int pdstage { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
