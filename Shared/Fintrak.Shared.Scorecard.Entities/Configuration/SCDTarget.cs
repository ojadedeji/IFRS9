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
    public partial class SCDTarget : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TargetId { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }


        [DataMember]
        [Required]
        public decimal Amount { get; set; }

        [DataMember]
        [Required]
        public DateTime Date { get; set; }


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
                return TargetId;
            }
        }

    }
}
