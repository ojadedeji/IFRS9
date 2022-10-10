using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class MPRBalanceSheetAdjustment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int BalancesheetAdjustmentId { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string Category { get; set; }

        [DataMember]
        [Required]
        public string CurrencyType { get; set; }


        [DataMember]
        [Required]
        public double ActualBal { get; set; }

        [DataMember]
        [Required]
        public double AverageBal { get; set; }

        [DataMember]
        public decimal Interest { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Code { get; set; }

        public int EntityId
        {
            get
            {
                return BalancesheetAdjustmentId;
            }
        }
    }
}
