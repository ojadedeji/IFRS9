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
    public partial class IfrsAccessEstimateRecoveryOutput : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public int Seq { get; set; }

        [DataMember]
        public string sector { get; set; }

        [DataMember]
        public int HistoryQuarter { get; set; }

        [DataMember]
        public double ExtematedRecovery { get; set; }

        [DataMember]
        public double Writeoff { get; set; }

        [DataMember]
        public double WeightedOutstndBal { get; set; }

        [DataMember]
        public double LGD { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
