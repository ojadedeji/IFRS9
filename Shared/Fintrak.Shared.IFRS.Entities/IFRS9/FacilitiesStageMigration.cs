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
    public partial class FacilitiesStageMigration : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public int PassDueDays { get; set; }

        [DataMember]
        public int StageDueToPDD { get; set; }

        [DataMember]
        public int StageDueToProbationalPeriod { get; set; }


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
