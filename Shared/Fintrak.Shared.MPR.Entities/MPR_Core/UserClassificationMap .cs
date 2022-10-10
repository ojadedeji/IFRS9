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
    public partial class UserClassificationMap : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int UserClassificationMapId { get; set; }

        [DataMember]
        [Required]
        public string LoginID { get; set; }

        [DataMember]
        public string ClassificationCode { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string ClassificationTypeCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return UserClassificationMapId;
            }
        }
    }
}
