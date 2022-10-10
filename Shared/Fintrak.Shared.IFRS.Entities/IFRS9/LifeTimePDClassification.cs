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
    public partial class LifeTimePDClassification : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LifeTimePDClassificationId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public double BaseLifetimePD { get; set; }


        [DataMember]
        public string BaseRating { get; set; }

        [DataMember]
        public double CurrentLifetimePD { get; set; }

        [DataMember]
        public string CurrentRating { get; set; }

        [DataMember]
        public int NotchDiff { get; set; }

        [DataMember]
        public int Probational_Period { get; set; }        

        [DataMember]
        public string Classification { get; set; }        

        [DataMember]
        public string Notes { get; set; }
                
        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LifeTimePDClassificationId;
            }
        }
    }
}
