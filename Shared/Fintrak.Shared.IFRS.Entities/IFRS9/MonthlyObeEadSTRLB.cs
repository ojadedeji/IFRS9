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
    public partial class MonthlyObeEadSTRLB : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public string OBEType { get; set; }

        [DataMember]
        public int OriginYr { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public double MarginalCCF { get; set; }

        [DataMember]
        public int Nodays { get; set; }

        [DataMember]
        public int NoOfDayMonth { get; set; }

        [DataMember]
        public int TTInDays { get; set; }

        [DataMember]
        public double EAD { get; set; }

        [DataMember]
        public int stage { get; set; }

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
