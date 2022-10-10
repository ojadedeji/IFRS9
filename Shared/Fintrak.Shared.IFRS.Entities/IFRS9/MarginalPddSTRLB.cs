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
    public partial class MarginalPddSTRLB  : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string Producttype { get; set; }

        [DataMember]
        public string subType { get; set; }

        [DataMember]
        public int OriginYear { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public int TenorInDays { get; set; }

        [DataMember]
        public double InterestAccrued { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public double EIR { get; set; }

        [DataMember]
        public double DiscountFactor { get; set; }

        [DataMember]
        public string Scenerio { get; set; }

        [DataMember]
        public double MarginalPD { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }


        public int EntityId {
            get {
                return ID;
            }
        }

    }
}
