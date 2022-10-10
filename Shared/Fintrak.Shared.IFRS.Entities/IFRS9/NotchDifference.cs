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
    public partial class NotchDifference : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int NotchDifferenceId { get; set; }

        [DataMember]
        public string InitialClassification { get; set; }

        [DataMember]
        public int StepMovement { get; set; }

        [DataMember]
        public string FinalClassification { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return NotchDifferenceId;
            }
        }
    }
}
