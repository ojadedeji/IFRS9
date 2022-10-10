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
    public partial class Regressionweights : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int weight_id { get; set; }

        [DataMember]
        public string labels { get; set; }

        [DataMember]
        public double weights { get; set; }

        [DataMember]
        public double pvalue { get; set; }

        //[DataMember]
        //public string label { get; set; }

        //[DataMember]
        //public double weight { get; set; }

        [DataMember]
        public double se { get; set; }

        [DataMember]
        public double tstat { get; set; }

        //[DataMember]
        //public double pval { get; set; }

        [DataMember]
        public double Lower_confidence_level { get; set; }

        [DataMember]
        public double Upper_confidence_level { get; set; }

        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return weight_id;
            }
        }
    }
}
