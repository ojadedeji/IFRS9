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
    public partial class AbcRatio : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int AbcRatioId { get; set; }

        [DataMember]
        [Required]
        public string Branch { get; set; }

        [DataMember]
        [Required]
        public double Percentage { get; set; }
       
        public int EntityId
        {
            get
            {
                return AbcRatioId;
            }
        }
    }
}
