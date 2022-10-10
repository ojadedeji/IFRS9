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
    public partial class IfrsCorporatePdSeries : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Sno { get; set; }

        [DataMember]
        public string CustID { get; set; }

        [DataMember]
        public string Rating { get; set; }         

        [DataMember]
        public int seq { get; set; }  

        [DataMember]
        public int Year { get; set; }  

        [DataMember]
        public string Period { get; set; }

        [DataMember]
        public double PDYear { get; set; }

        [DataMember]
        public double MarginalDefaultPD { get; set; }

        [DataMember]
        public double MarginalPD_BEST { get; set; }

        [DataMember]
        public double MarginalPD_Downturn { get; set; }

        [DataMember]
        public double MarginalPD_Optimistic { get; set; }

        [DataMember]
        public double SurvivalPD_BEST { get; set; }

        [DataMember]
        public double SurvivalPD_Downturn { get; set; }

        [DataMember]
        public double SurvivalPD_Optimistic { get; set; }

        [DataMember]
        public double LifeTimePD_BEST { get; set; }

        [DataMember]
        public double LifeTimePD_Downturn { get; set; }

        [DataMember]
        public double LifeTimePD_Optimistic { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Sno;
            }
        }
    }
}