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
    public partial class CashFlowTB : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)] public int ID { get; set; }
        [DataMember] public string Refno { get; set; }
        [DataMember] public string Component { get; set; }
        [DataMember] public DateTime Start_date { get; set; }
        [DataMember] public DateTime Due_Date { get; set; }
        [DataMember] public double Amount_Due { get; set; }
        [DataMember] public double amount_settled { get; set; }
        [DataMember] public double Over_due { get; set; }
        [DataMember] public DateTime Rundate { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
