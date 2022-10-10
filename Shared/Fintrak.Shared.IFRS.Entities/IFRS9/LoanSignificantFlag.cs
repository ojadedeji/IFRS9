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
    public partial class LoanSignificantFlag : EntityBase, IIdentifiableEntity
    {
/*
      Id
      LoanClassificationId
      ProductType
      SubType
      SICR_Flag
      SignificantNo

*/
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public int? LoanClassificationId { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string SICR_Flag { get; set; }

        [DataMember]
        public int SignificantNo { get; set; }

        [DataMember]
        public bool Active { get; set; }


        public int EntityId {
            get {
                return Id;
            }
        }

    }
}
