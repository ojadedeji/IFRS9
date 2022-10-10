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
    public partial class FiscalYear : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FiscalYearId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public DateTime StartDate { get; set; }

        [DataMember]
        [Required]
        public DateTime EndDate { get; set; }

        [DataMember]
        public bool Closed { get; set; }

        public int EntityId
        {
            get
            {
                return FiscalYearId;
            }
        }
    }
}
