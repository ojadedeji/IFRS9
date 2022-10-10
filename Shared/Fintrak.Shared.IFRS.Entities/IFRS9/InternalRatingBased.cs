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
    public partial class InternalRatingBased : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int InternalRatingBasedId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public double PD { get; set; }

        [DataMember]
        public double PD_LowerBoundary { get; set; }

        [DataMember]
        public double PD_UpperBoundary { get; set; }


        [DataMember]
        public string Description { get; set; }
               

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public int Rank { get; set; }

         [DataMember]
        public bool Low_Level_Credit { get; set; }

         //[DataMember]
         //public string SP_PD_Structure { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return InternalRatingBasedId;
            }
        }
    }
}
