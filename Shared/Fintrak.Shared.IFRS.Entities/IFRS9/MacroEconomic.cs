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
    public partial class MacroEconomic : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int MacroEconomicId { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string Sector_Code { get; set; }

        [DataMember]
        [Required]
        public double? Variable1 { get; set; }

        [DataMember]
        public double? Variable2 { get; set; }

        [DataMember]
        public double? Variable3 { get; set; }


        [DataMember]
        public double? Variable4 { get; set; }

        [DataMember]
        public double? Variable5 { get; set; }
               

        //[DataMember]
        //public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return MacroEconomicId;
            }
        }
    }
}
