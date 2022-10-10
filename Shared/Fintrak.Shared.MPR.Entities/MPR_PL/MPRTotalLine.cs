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
    public partial class MPRTotalLine : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TotallineId { get; set; }



        [DataMember]
        [Required]
        public string Name { get; set; }


        [DataMember]       
        public string Color { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return TotallineId;
            }
        }

    }
}
