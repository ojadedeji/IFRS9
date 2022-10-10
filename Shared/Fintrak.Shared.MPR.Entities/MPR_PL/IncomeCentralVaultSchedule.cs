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
    public partial class IncomeCentralVaultSchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int IncomeCentralVaultScheduleId { get; set; }        
        [DataMember]
        [ReadOnly(true)]
        public string BranchCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string Currency { get; set; }

        [DataMember]
        public DateTime DatePosted { get; set; }

        [DataMember]
        public decimal Volume { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Period { get; set; }

        public int EntityId
        {
            get
            {
                return IncomeCentralVaultScheduleId;
            }
        }

    }
}
