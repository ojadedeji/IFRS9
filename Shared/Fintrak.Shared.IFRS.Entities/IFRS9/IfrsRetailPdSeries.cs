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
    public partial class IfrsRetailPdSeries : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int PdSeriesId { get; set; }

        [DataMember]
        public string refno { get; set; }

        [DataMember]
        public int ClassificationStage { get; set; }         

        [DataMember]
        public double LifeTimePD_BEST { get; set; }  

        [DataMember]
        public double LifeTimePD_Downturn { get; set; }  

        [DataMember]
        public double LifeTimePD_Optimistic { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public string Category { get; set; }   



        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return PdSeriesId;
            }
        }
    }
}