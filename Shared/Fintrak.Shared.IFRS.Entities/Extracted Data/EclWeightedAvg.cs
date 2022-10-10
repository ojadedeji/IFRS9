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
    public partial class EclWeightedAvg : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ECLWATempID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public Nullable<double> OldFinalECLWeightAvg { get; set; }

        [DataMember]
        public Nullable<double> FinalECLWeightAvg { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ECLWATempID;
            }
        }
    }
}
