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
    public partial class HistoricalDefaultedAccounts : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int DefaultedAccountId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }
        [DataMember]
     
        [Required]
        public string Sector { get; set; }

        [DataMember]
        [Required]
        public decimal ODLimit { get; set; }

        [DataMember]
        public double PrincipalOutstandingBal { get; set; }

        [DataMember]
        public double BalanceOnRefDate { get; set; }

        [DataMember]
        public double BalanceOnDefaultDate { get; set; }

        //[DataMember]
        //public double PastDueAmount { get; set; }

        //[DataMember]
        //public int PastDueDays { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        //[DataMember]
        ////public decimal Rate { get; set; }

        //[DataMember]
        //[Required]
        //public DateTime ValueDate { get; set; }

        //[DataMember]
        //[Required]
        //public DateTime MaturityDate { get; set; }

        //[DataMember]
        //public int TenorDays { get; set; }

        //[DataMember]
        //public int TenorMonth { get; set; }

        //[DataMember]
        //public int Period { get; set; }

        //[DataMember]
        //public int Year { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

        //[DataMember]
        //public string CompanyCode { get; set; }

        [DataMember]
        public string Classification { get; set; }

        //[DataMember]
        //public string Segment { get; set; }

        //[DataMember]
        //public string SubSegment { get; set; }

       
        public int EntityId
        {
            get
            {
                return DefaultedAccountId;
            }
        }
    }
}
