using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class MPRBalanceSheet : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int BalanceSheetId { get; set; }

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
        public string CaptionName { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

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
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public decimal ActualBal { get; set; }

        [DataMember]
        [Required]
        public decimal AverageBal { get; set; }

        [DataMember]
        public decimal Interest { get; set; }

        [DataMember]
        public double EffIntRate { get; set; }

        [DataMember]
        public decimal Pool { get; set; }

        [DataMember]
        public double PoolRate { get; set; }

        [DataMember]
        public double ContractRate { get; set; }

        [DataMember]
        public string VolumeGL { get; set; }

        [DataMember]
        public string InterestGL { get; set; }

        [DataMember]
        public string EntryStatus { get; set; }

        [DataMember]
        public double MaxRate { get; set; }

        [DataMember]
        public decimal PenalCharge { get; set; }

        [DataMember]
        public double PenalRate { get; set; }

        [DataMember]
        public string AcctStatus { get; set; }

        [DataMember]
        public string CreditRating { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return BalanceSheetId;
            }
        }
    }
}
