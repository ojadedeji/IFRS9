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
    public partial class MacroEconomicsNPL : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int macroeconomicnplId { get; set; }

        [DataMember]
        [Required]
        public int Seq { get; set; }

        [DataMember]
        public int Year { get; set; }


        [DataMember]
        public double NPL { get; set; }


        [DataMember]
        public string Scenerio { get; set; }


        [DataMember]
        public double HistoricalAvg { get; set; }


        [DataMember]
        public DateTime? Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return macroeconomicnplId;
            }
        }
    }
}
