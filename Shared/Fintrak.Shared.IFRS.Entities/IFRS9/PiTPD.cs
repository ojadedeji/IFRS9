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
    public partial class PiTPD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int PiTPDId { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Yr2017 { get; set; }

        [DataMember]
        public string Yr2018 { get; set; }
        [DataMember]
        public string Yr2019 { get; set; }
        [DataMember]
        public string Yr2020 { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return PiTPDId;
            }
        }
    }
}
