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
    public partial class IfrsLoanIRRData : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public double IRR { get; set; }

        [DataMember]
        public double pvalue { get; set; }

        [DataMember]
        public double NominalRate { get; set; }

        [DataMember]
        public DateTime FirstPaymentDate { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public double InitialIRR { get; set; }

        [DataMember]
        public int NoOfPeriods { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public double PLR { get; set; }

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
