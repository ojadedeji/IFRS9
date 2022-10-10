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
    public partial class PDMigrationMatrixFinal : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public DateTime CurrentDate { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public double RG1 { get; set; }

        [DataMember]
        public double RG2 { get; set; }

        [DataMember]
        public double RGD { get; set; }

        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
