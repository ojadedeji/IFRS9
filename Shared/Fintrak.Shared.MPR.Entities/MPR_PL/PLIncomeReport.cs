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
    public partial class PLIncomeReport : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string TransId { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public DateTime TransDate { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string Narrative { get; set; }

        [DataMember]
        [Required]
        public string TeamCode { get; set; }


        [DataMember]
        [Required]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string BranchCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string GLCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string GLAccount { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string CustCode { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string Caption { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }

        [DataMember]
        public string AccountTitle { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public decimal Amount { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string EntryStatus { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public DateTime martdate { get; set; }

        [DataMember]
        public string StaffID { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }

    }
}
