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

namespace Fintrak.Shared.Core.Entities
{
    public partial class CheckifrsDataAvailability : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CheckDataId { get; set; }

        [DataMember]
        [Required]
        public DateTime ExtractionDate { get; set; }

        [DataMember]
        [Required]
        public string Package { get; set; }

        [DataMember]
        [Required]
        public int Status { get; set; }


        [DataMember]
        [Required]
        public int RecCount { get; set; }


        [DataMember]
        [Required]
        public string ColorCode { get; set; }

        public int EntityId
        {
            get
            {
                return CheckDataId;
            }
        }
    }
}
