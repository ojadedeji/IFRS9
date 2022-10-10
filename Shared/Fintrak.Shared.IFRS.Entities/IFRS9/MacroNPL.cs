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
    public partial class MacroNPL : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int MacroID { get; set; }

        [DataMember]
        [Required]
        public DateTime Period { get; set; }

        [DataMember]
        [Required]
        public double Inflation { get; set; }


        [DataMember]
        [Required]
        public double CrudeOilPrice { get; set; }

        [DataMember]
        [Required]
        public double UnemploymentRatio { get; set; }

        [DataMember]
        [Required]
        public string Scenario { get; set; }


     
        [DataMember]
        [Required]
        [DefaultValue(false)]
        public bool Approved { get; set; } = false;


      
        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return MacroID;
            }
        }
    }
}
