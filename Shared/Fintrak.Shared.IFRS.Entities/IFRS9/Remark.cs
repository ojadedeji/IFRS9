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
    public partial class Remark : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ErrorList { get; set; }

        [DataMember]

        public string sqlcmd { get; set; }

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
