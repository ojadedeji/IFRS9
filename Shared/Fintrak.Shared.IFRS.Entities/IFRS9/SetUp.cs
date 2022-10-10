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
    public partial class SetUp : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SetUpId { get; set; }

        [DataMember]
        public string Parameter { get; set; }

        [DataMember]
        public string Type { get; set; }

        //[DataMember]
        //public double? Value_Number { get; set; }

        //[DataMember]
        //public DateTime? Value_Date { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SetUpId;
            }
        }
    }
}
