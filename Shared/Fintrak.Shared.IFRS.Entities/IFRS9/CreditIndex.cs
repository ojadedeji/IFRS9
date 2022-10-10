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
    public partial class CreditIndex : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public int foreacst_date { get; set; }

        [DataMember]
        public double creditindex { get; set; }

        [DataMember]
        public string scenerio { get; set; }

        [DataMember]
        public DateTime forcast_date { get; set; }

        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
