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
    public partial class RegressionOutput : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int RegressionOutputId { get; set; }

        [DataMember]
        public string Variable { get; set; }

        [DataMember]
        public double Rsquare { get; set; }

        [DataMember]
        public double Fstatistic { get; set; }

        [DataMember]
        public double Pvalue { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return RegressionOutputId;
            }
        }
    }
}
