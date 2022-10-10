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
    public partial class BalanceSheetBudgetOfficer : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int BudgetId { get; set; }

        [DataMember]
        [Required]
        public string CaptionName { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        [Required]
        public decimal Mth1 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth2 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth3 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth4 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth5 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth6 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth7 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth8 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth9 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth10 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth11 { get; set; }

        [DataMember]
        [Required]
        public decimal Mth12 { get; set; }

 
      
        public int EntityId
        {
            get
            {
                return BudgetId;
            }
        }

    }
}
