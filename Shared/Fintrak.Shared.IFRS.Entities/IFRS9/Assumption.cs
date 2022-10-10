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
    public partial class Assumption : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int InstrumentID { get; set; }

        [DataMember]
        public string Instrument { get; set; }

        [DataMember]
        public int Highest_Level_of_Speculative { get; set; }

        [DataMember]
        public int Notches_for_sicr { get; set; }

        [DataMember]
        public string Default_Rating { get; set; }

        [DataMember]
        public string Assumed_DefaultRating { get; set; }

        [DataMember]
        public double Assumed_EIR { get; set; }

        [DataMember]
        public int Assumed_Tenor { get; set; }

        [DataMember]
        public DateTime Assumed_MaturityDate { get; set; }

        [DataMember]
        public DateTime Assumed_StartDate { get; set; }

        [DataMember]
        public double Assumed_CCF_Guarantee { get; set; }

        [DataMember]
        public double Assumed_CCF_LCs { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return InstrumentID;
            }
        }
    }
}
