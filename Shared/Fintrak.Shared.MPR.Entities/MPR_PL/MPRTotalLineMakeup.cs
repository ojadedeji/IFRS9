using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

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
    public partial class MPRTotalLineMakeUp : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TotalLineMakeUpId { get; set; }



        [DataMember]
        [Required]
        public string TotalLine { get; set; }


        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

  

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return TotalLineMakeUpId;
            }
        }

    }
}
