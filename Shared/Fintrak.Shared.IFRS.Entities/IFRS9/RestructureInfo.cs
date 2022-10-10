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
    public partial class RestructureInfo : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]

        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }


        [DataMember]

        public DateTime MaturityDate { get; set; }

        [DataMember]

        public double Rate { get; set; }

        [DataMember]

        public double OriginalEIR { get; set; }


        [DataMember]

        public double Outstandingbal { get; set; }


        [DataMember]

        public int Repayfreq { get; set; }

        [DataMember]

        public int InterestRepayfreq { get; set; }

        [DataMember]

        public int NoRepayments { get; set; }

        [DataMember]
        public DateTime PrincFirstPmtDate { get; set; }

        [DataMember]
        public DateTime InterestFirstPmtDate { get; set; }

        [DataMember]

        public bool Flag { get; set; }

        //[DataMember] InterestRepayFreq
        //public string CompanyCode { get; set; }


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