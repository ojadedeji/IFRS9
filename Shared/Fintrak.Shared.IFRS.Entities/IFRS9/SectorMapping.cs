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
    public partial class SectorMapping : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SectorMapping_Id { get; set; }

        [DataMember]
        [Required]
        public string CBNSector { get; set; }

        [DataMember]
        public string SubSector { get; set; }

        [DataMember]
        public string LGDSectorMapping { get; set; }

        [DataMember]
        public string CCFMapping { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SectorMapping_Id;
            }
        }
    }
}
