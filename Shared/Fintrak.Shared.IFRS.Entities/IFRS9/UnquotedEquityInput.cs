using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class UnquotedEquityInput : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public double Bookvalue { get; set; }

        [DataMember]
        public double Earnings { get; set; }

        [DataMember]
        public double NumberOfShares { get; set; }

        [DataMember]
        public string EvaluationType { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
