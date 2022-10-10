using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsMevForcastabp : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Forecast_id { get; set; }

        [DataMember]
        public DateTime Periodic_date { get; set; }

        [DataMember]
        public string Period_in_quarter { get; set; }

        [DataMember]
        public double Factor { get; set; }

        [DataMember]
        public string Mevid { get; set; }

        public int EntityId
        {
            get
            {
                return Forecast_id;
            }
        }
    }
}
