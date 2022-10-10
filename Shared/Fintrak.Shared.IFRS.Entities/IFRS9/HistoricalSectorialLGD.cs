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
    public partial class HistoricalSectorialLGD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int HistoricalSectorialLGDId { get; set; }

        [DataMember]
        [Required]
        public string SectorCode { get; set; }
        [DataMember]
     
        [Required]
        public string SectorName { get; set; }

        [DataMember]
        [Required]
        public double LGD { get; set; }

        [DataMember]
        [Required]
        public double AvgLGD{ get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return HistoricalSectorialLGDId;
            }
        }
    }
}
