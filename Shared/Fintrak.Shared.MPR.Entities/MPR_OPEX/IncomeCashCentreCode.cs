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
    public partial class IncomeCashCentreCode : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int IncomeCashCentreCodeId { get; set; }

        [DataMember]
        [Required]
        public string CashCentreCode { get; set; }

        [DataMember]
        [Required]
        public string BranchCode { get; set; }

        public int EntityId
        {
            get
            {
                return IncomeCashCentreCodeId;
            }
        }

    }
}
