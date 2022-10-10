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
    public partial class ECLComparism : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ECLComparismId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string Bucket { get; set; }

        [DataMember]
        public string Stressed_Bucket { get; set; }

        [DataMember]
        public double LifeTimePD { get; set; }

        [DataMember]
        public double Stressed_LifeTimePD { get; set; }

        [DataMember]
        public double AnnualPD { get; set; }             

        [DataMember]
        public double EAD { get; set; }

        [DataMember]
        public double UndrawnAmount { get; set; }

        [DataMember]
        public double BalanceOutstanding { get; set; }

        [DataMember]
        public double CashShortFall { get; set; }

        [DataMember]
        public double Stressed_CashShortFall { get; set; }

        [DataMember]
        public double ImpairmentCharge { get; set; }

        [DataMember]
        public double Stressed_ImpairmentCharge { get; set; }

        [DataMember]
        public double ExpectedLoss { get; set; }

        [DataMember]
        public double Stressed_ExpectedLoss { get; set; }

        [DataMember]
        public double DiscountedValue { get; set; }

        [DataMember]
        public double Stressed_DiscountedValue { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ECLComparismId;
            }
        }
    }
}
