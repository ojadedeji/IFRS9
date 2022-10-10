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
    public partial class MacroeconomicsVariableScenario : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int MacroeconomicsId { get; set; }

        [DataMember]
        public string Variable { get; set; }

        [DataMember]
        public string Frequency { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public double Optimistic { get; set; }

        [DataMember]
        public double Best { get; set; }

        [DataMember]
        public double Downturn { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public int Flag { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return MacroeconomicsId;
            }
        }
    }
}
