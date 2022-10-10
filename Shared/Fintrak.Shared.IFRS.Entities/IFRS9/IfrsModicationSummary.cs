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
    public partial class IfrsModicationSummary : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string refno { get; set; }

        [DataMember]
        public double Modificationgain_loss { get; set; }

        //[DataMember]
        //public double modification_test { get; set; }

        [DataMember]
        public string Treatment { get; set; }

        [DataMember]
        public string comment { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
