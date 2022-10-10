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
    public partial class OpexCheckList : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ChecklistId { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public decimal Actual { get; set; }

  
        public int EntityId
        {
            get
            {
                return ChecklistId;
            }
        }

    }
}
