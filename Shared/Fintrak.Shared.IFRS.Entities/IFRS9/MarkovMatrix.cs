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
    public partial class MarkovMatrix : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int MarkovMatrixId { get; set; }

        [DataMember]
        [Required]
        public string Sector { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public double InitialPD { get; set; }
        [DataMember]
        public double InitialNonPD { get; set; }
        [DataMember]
        public double PDmatrix { get; set; }
        [DataMember]
        public double NPDmatrix { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return MarkovMatrixId;
            }
        }
    }
}
