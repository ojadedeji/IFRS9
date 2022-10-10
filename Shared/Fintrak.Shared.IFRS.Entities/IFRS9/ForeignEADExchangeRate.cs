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
    public partial class ForeignEADExchangeRate : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ForeignEADExchangeRateId { get; set; }

        [DataMember]
        public DateTime IntRate_date { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double InterestRate { get; set; }


        [DataMember]
        public string Reference { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ForeignEADExchangeRateId;
            }
        }
    }
}
