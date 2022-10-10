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
    public partial class IfrsInvestment : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }


        [DataMember]
        [Browsable(false)]
        public int INVType_Code { get; set; }


        [DataMember]
        [Browsable(false)]
        public string TypeCode { get; set; }


        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        public double  Rate { get; set; }

        [DataMember]
        public double CureRate { get; set; }


        [DataMember]
        public DateTime RunDate { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
