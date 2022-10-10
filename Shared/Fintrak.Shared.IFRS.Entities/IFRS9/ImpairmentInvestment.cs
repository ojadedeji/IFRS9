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
    public partial class ImpairmentInvestment : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Investment_Id { get; set; }

        [DataMember]
        public string cust_id { get; set; }

        [DataMember]
        [Required]
        public string counterparty { get; set; }

        [DataMember]
        [Required]
        public string asset_name { get; set; }

        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public double stage_allocation { get; set; }

        [DataMember]
        public double outstanding_bal { get; set; }

        [DataMember]
        public double best_ecl { get; set; }

        [DataMember]
        public double Optimistic_ecl { get; set; }

        [DataMember]
        public double downturn_ecl { get; set; }

        [DataMember]
        public double impairment { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Investment_Id;
            }
        }
    }
}
