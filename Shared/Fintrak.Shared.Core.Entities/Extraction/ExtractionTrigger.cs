using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Core.Entities
{
    public partial class ExtractionTrigger : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ExtractionTriggerId { get; set; }

        [DataMember]
        [Required]
        public int ExtractionJobId { get; set; }

        [DataMember]
        [Required]
        public int ExtractionId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public PackageStatus Status  { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public DateTime? RunTime { get; set; }

        [DataMember]
        public bool NeedToArchive { get; set; }

        public int EntityId
        {
            get
            {
                return ExtractionTriggerId;
            }
        }
    }
}
