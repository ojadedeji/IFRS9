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
    public partial class CollateralInput : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Collateral_Id { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public double Collateral_haircut { get; set; }

        [DataMember]
        public double Collateral_Growth_rate { get; set; }

        [DataMember]
        public double Cost_of_recovery { get; set; }

        [DataMember]
        public double Time_of_recovery { get; set; }

        [DataMember]
        public string catergory { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Collateral_Id;
            }
        }
    }
}
