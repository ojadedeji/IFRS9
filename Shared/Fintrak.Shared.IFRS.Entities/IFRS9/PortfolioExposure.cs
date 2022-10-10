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
    public partial class PortfolioExposure : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int PortfolioId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public double Exposure { get; set; }


        [DataMember]
        public int RecCount { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return PortfolioId;
            }
        }
    }
}
