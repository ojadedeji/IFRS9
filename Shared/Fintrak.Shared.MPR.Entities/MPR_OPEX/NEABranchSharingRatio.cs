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
    public partial class NEABranchSharingRatio : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int NEABranchSharingRatioId { get; set; }

        [DataMember]
        [Required]
        public string OwnerBranch { get; set; }


        [DataMember]
        public string Beneficiary { get; set; }

        [DataMember]
        public decimal Ratio { get; set; }

        [DataMember]
        public string GL { get; set; }

        public int EntityId
        {
            get
            {
                return NEABranchSharingRatioId;
            }
        }

    }
}
