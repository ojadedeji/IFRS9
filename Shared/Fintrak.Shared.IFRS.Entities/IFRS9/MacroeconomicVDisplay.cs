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
    public partial class MacroeconomicVDisplay : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int MacroVariableDisplayId { get; set; }

        [DataMember]
        public string Sector_Code { get; set; }

        [DataMember]
        public string Sector_Name { get; set; }
        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        public double Exchange_Rate { get; set; }

        [DataMember]
        public double GDP { get; set; }

        [DataMember]
        public double Oil_Price { get; set; }

        [DataMember]
        public double Unemployment_Rate { get; set; }
        
        [DataMember]
        public double Inflation_Rate { get; set; }
        [DataMember]
        public string VType { get; set; }
        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return MacroVariableDisplayId;
            }
        }
    }
}
