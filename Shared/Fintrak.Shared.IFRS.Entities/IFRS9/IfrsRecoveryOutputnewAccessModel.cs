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
    public partial class IfrsRecoveryOutputnewAccessModel : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string BorrowedID { get; set; }

        [DataMember]
        public double Recovery { get; set; }

        [DataMember]
        public int HistoryQuarter { get; set; }

        [DataMember]
        public DateTime EndOfMonth { get; set; }

        [DataMember]
        public string sector { get; set; }

        [DataMember]
        public string mapped_sector { get; set; }

        [DataMember]
        public string portfolio { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
