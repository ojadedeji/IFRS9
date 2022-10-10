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
    public partial class OpexGLBasis : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int OpexGLBasisId { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        [Required]
        public string MISCode { get; set; }

        [DataMember]
        public double Basis { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public bool Active { get; set; }

        
        public int EntityId
        {
            get
            {
                return OpexGLBasisId;
            }
        }

    }
}
