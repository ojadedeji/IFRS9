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
    public partial class Mevlisting : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int listing_id { get; set; }

        [DataMember]
        public string mev { get; set; }

        [DataMember]
        public string mev_code { get; set; }

        [DataMember]
        public bool probability_weighted { get; set; }

        [DataMember]
        public bool Dependent_variable { get; set; }

        [DataMember]
        public string future_opt_sign { get; set; }

        [DataMember]
        public string future_down_sign { get; set; }

        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return listing_id;
            }
        }
    }
}
