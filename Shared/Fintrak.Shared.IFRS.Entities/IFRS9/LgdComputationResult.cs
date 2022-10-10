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
    public partial class LgdComputationResult : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public double Amt_LCY { get; set; }

        [DataMember]
        public double AMT_FCY { get; set; }

        [DataMember]
        public double eir { get; set; }

        [DataMember]
        public double CollateralValue { get; set; }

        [DataMember]
        public double CollateralgrowthRate { get; set; }

        [DataMember]
        public double CollateralgrowthValue { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public DateTime Collateral_Realiazation_Date { get; set; }

        [DataMember]
        public double CollateralHaircut { get; set; }

        [DataMember]
        public double Realization_period { get; set; }

        [DataMember]
        public double Cost_of_Recovery_Rate { get; set; }

        [DataMember]
        public double Hair_cut_Cost { get; set; }

        [DataMember]
        public double Cost_of_recovery { get; set; }

        [DataMember]
        public double Values_at_rundate { get; set; }

        [DataMember]
        public double Net_Cash_Flow { get; set; }

        [DataMember]
        public double DiscountedValue { get; set; }

        [DataMember]
        public double LGD { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

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

