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
    public partial class TrialBalanceConsolidated : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TrialBalanceId { get; set; }

        [DataMember]        
        public string BranchCode { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string GLSubHeadCode { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public decimal Debit { get; set; }

        [DataMember]
        public decimal Credit { get; set; }

        [DataMember]
        public decimal LCY_Debit { get; set; }

        [DataMember]
        public decimal LCY_Credit { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public decimal LCY_Balance { get; set; }

        [DataMember]
        public string GLType { get; set; }

        [DataMember]
        public decimal RevaluationDiff { get; set; }

        [DataMember]
        public DateTime TransDate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Sub_GL { get; set; }

        [DataMember]
        public string AdjustmentCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return TrialBalanceId;
            }
        }
    }
}
