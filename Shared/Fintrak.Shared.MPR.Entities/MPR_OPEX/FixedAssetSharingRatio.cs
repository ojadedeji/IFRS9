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
    public partial class FixedAssetSharingRatio : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int FixedAssetSharingRatioId { get; set; }

        [DataMember]
        [Required]
        public string Branch { get; set; }

        [DataMember]
        [Required]
        public string SBUCode { get; set; }

        [DataMember]
        public decimal Ratio { get; set; }



        public int EntityId
        {
            get
            {
                return FixedAssetSharingRatioId;
            }
        }

    }
}
