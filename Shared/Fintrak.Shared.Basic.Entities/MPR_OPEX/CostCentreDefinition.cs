using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class CostCentreDefinition : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CCDefinitionId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return CCDefinitionId;
            }
        }

    }
}
