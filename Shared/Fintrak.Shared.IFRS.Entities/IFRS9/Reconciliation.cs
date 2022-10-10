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
    public partial class Reconciliation : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ReconciliationId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public double NCIAnnualGrossAmount { get; set; }

        [DataMember]
        public double NCIAnnualAllowanceECL { get; set; }

        [DataMember]
        public double NCILifetimeGrossAmount { get; set; }
             
        [DataMember]
        public double NCILifetimeAllowanceECL { get; set; }

        [DataMember]
        public double CILifetimeGrossAmount { get; set; }

        [DataMember]
        public double CILifetimeAllowanceECL { get; set; }

        [DataMember]
        public double TotalLifetimeGrossAmount { get; set; }

        [DataMember]
        public double TotalLifetimeAllowanceECL { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ReconciliationId;
            }
        }
    }
}
