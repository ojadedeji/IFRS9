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
    public partial class IfrsLifetimePDStages : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public DateTime Date_pmt { get; set; }

        [DataMember]
        public double CumulativeSP { get; set; }

        [DataMember]
        public double MarginalSP { get; set; }

        [DataMember]
        public double MarginalDR { get; set; }

        [DataMember]
        public double MarginalDRBest { get; set; }

        [DataMember]
        public double MarginalDROptimistic { get; set; }

        [DataMember]
        public double MarginalDRDownturn { get; set; }

        [DataMember]
        public double CummPDBest { get; set; }

        [DataMember]
        public double CummPDOptimistic { get; set; }

        [DataMember]
        public double CummPDDownturn { get; set; }

        [DataMember]
        public double LifetimePDBest { get; set; }

        [DataMember]
        public double LifetimePDOptimistic { get; set; }

        [DataMember]
        public double LifetimePDDownturn { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
