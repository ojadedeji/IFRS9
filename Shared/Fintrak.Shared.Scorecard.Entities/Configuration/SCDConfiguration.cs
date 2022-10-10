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

namespace Fintrak.Shared.Scorecard.Entities
{
    public partial class SCDConfiguration : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ConfigurationId { get; set; }

        [DataMember]
        [Required]
        public OperationMode Mode { get; set; }

        [DataMember]
        [Required]
        public PeriodType PeriodType { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string TeamClassificationTypeCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return ConfigurationId;
            }
        }

    }
}
