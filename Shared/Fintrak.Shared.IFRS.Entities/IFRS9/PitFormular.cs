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
    public partial class PitFormular : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int PitFormularId { get; set; }

        [DataMember]
        public string Sector_code { get; set; }

        [DataMember]
        public string Equation { get; set; }

        [DataMember]
        public double ComputedPd { get; set; }

        [DataMember]
        public string Type { get; set; }
             

        [DataMember]
        public DateTime Rundate { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return PitFormularId;
            }
        }
    }
}
