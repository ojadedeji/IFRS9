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
    public partial class SPCumulativePD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SPCumulative_Id { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public int Years { get; set; }

        [DataMember]
        public double Value { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SPCumulative_Id;
            }
        }
    }
}
