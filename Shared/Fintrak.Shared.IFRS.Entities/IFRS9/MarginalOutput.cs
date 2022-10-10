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
    public partial class MarginalOutput : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string AssetDescription { get; set; }

        [DataMember]
        public string AssetType { get; set; }

        [DataMember]
        public string Counterparty { get; set; }

        [DataMember]
        public string RatingAgency { get; set; }

        [DataMember]
        public string CreditRating { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember] public decimal MO1 { get; set; }
        [DataMember] public decimal MO2 { get; set; }
        [DataMember] public decimal MO3 { get; set; }
        [DataMember] public decimal MO4 { get; set; }
        [DataMember] public decimal MO5 { get; set; }
        [DataMember] public decimal MO6 { get; set; }
        [DataMember] public decimal MO7 { get; set; }
        [DataMember] public decimal MO8 { get; set; }
        [DataMember] public decimal MO9 { get; set; }
        [DataMember] public decimal MO10 { get; set; }
        [DataMember] public decimal MO11 { get; set; }
        [DataMember] public decimal MO12 { get; set; }
        [DataMember] public decimal MO13 { get; set; }
        [DataMember] public decimal MO14 { get; set; }
        [DataMember] public decimal MO15 { get; set; }

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
