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
    public partial class ProbabilityWeighted : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ProbabilityWeighted_Id { get; set; }

        [DataMember]
        public double Optimistic { get; set; }

        [DataMember]
        public double Best { get; set; }

        [DataMember]
        public double Downturn { get; set; }

        [DataMember]
        [Required]
        public string InstrumentType { get; set; }

        [DataMember]
       
        public string Category { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }

        [DataMember]
        [Required]
        public string SubType { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ProbabilityWeighted_Id;
            }
        }
    }
}
