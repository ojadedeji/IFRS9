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
    public partial class LoanRestructurePVCashFlow : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public double OpeningBalance { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public int DNY { get; set; }

        [DataMember]
        public int NoDays { get; set; }

        [DataMember]
        public int TotalNoDays { get; set; }

        [DataMember]
        public double YID { get; set; }

        [DataMember]
        public double DiscountFactor { get; set; }

        [DataMember]
        public double Amt_int_pay { get; set; }

        [DataMember]
        public double Amt_prin_pay { get; set; }

        [DataMember]
        public double Amt_pmt { get; set; }

        [DataMember]
        public double CummulativeInterest { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double PVCashFlow { get; set; }

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
