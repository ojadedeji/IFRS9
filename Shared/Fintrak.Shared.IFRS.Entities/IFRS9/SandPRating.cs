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
    public partial class SandPRating : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SandPRating_Id { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public double Rating { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SandPRating_Id;
            }
        }
    }
}
