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
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class UserMIS : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int UserMISId { get; set; }

        [DataMember]
        [Required]
        public string LoginID { get; set; }

        [DataMember]
        public string ProfitCenterDefinitionCode { get; set; }

        [DataMember]
        public string ProfitCenterMisCode { get; set; }

        [DataMember]
        public string CostCenterDefinitionCode { get; set; }

        [DataMember]
        public string CostCenterMisCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return UserMISId;
            }
        }
    }
}
