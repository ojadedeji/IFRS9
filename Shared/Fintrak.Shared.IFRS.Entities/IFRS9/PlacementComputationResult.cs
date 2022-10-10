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
    public partial class PlacementComputationResult : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public DateTime BookingDate { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public double TransactionAmount { get; set; }

        [DataMember]
        public double InterestRate { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public double LCY_Amount { get; set; }

        [DataMember]
        public double Days_in_Holding { get; set; }

        [DataMember]
        public double Interest { get; set; }

        [DataMember]
        public double AmortisedCost { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

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

