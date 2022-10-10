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
    public partial class ECLInputRetail : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int EclInputRetailId { get; set; }

        [DataMember]
        [Required]
        public string account_number { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public double EIR { get; set; }

        [DataMember]
        public double SeriesValue { get; set; }

        [DataMember]
        public double Amount { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return EclInputRetailId;
            }
        }
    }
}
