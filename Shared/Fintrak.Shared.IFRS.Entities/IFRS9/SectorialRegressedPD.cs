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
    public partial class SectorialRegressedPD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SectorialRegressedPDId { get; set; }

        [DataMember]
        public string SectorCode { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public double AnnualPD { get; set; }

        [DataMember]
        public double LifeTimePD { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string Description { get; set; }
             

        [DataMember]
        public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SectorialRegressedPDId;
            }
        }
    }
}
