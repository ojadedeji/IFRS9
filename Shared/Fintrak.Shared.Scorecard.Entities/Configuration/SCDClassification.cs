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
    public partial class SCDKPIClassification : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ClassificationId { get; set; }

        [DataMember]
        [Required]
        public string KPICode { get; set; }


        [DataMember]
        [Required]
        public string TeamClassificationCode { get; set; }

        [DataMember]
        [Required]
        public string CategoryCode { get; set; }


        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

      
        public int EntityId
        {
            get
            {
                return ClassificationId;
            }
        }

    }
}
