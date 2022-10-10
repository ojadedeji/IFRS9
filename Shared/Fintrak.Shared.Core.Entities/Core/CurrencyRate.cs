using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Core.Framework;
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

namespace Fintrak.Shared.Core.Entities
{
    public partial class CurrencyRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CurrencyRateId { get; set; }

        [DataMember]
        [Required]
        public int CurrencyId { get; set; }

        [DataMember]
        public string Frequency { get; set; }

        [DataMember]
        [Required]
        public int RateTypeId { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public Nullable<DateTime> Rundate { get; set; }

        

      
        public int EntityId
        {
            get
            {
                return CurrencyRateId;
            }
        }
    }
}
