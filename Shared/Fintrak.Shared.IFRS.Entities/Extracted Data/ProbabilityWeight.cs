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
    public partial class ProbabilityWeight : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ProbabilityWeighId { get; set; }

        [DataMember]
        public Nullable<double> Mean { get; set; }

        [DataMember]
        public Nullable<double> StandardDeviation { get; set; }

        [DataMember]
        public Nullable<double> LowerLimit { get; set; }

        [DataMember]
        public Nullable<double> UpperLimit { get; set; }

        [DataMember]
        public Nullable<double> DownTurn { get; set; }

        [DataMember]
        public Nullable<double> Upturn { get; set; }

        [DataMember]
        public Nullable<double> Base { get; set; }

        [DataMember]
        public double Loc { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ProbabilityWeighId;
            }
        }
    }
}
