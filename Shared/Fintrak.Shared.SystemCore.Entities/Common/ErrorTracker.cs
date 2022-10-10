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

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class ErrorTracker : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ErrorTrackerId { get; set; }

        [DataMember]
        [Required]
        public string ErrMessage { get; set; }

        [DataMember]
        [Required]
        public string StoredProcedureName { get; set; }

        [DataMember]
        [Required]
        public string UserId { get; set; }
            
        public int EntityId
        {
            get
            {
                return ErrorTrackerId;
            }
        }

    }
}
