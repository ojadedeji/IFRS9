using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class RatingMapping : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int RatingMappingId { get; set; }

        [DataMember]
        [Required]
        public int Credit_Risk_Id { get; set; }

        [DataMember]
        [Required]
        public int External_Rating_Id { get; set; }

        //[DataMember]
        //public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return RatingMappingId;
            }
        }
    }
}
