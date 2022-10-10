using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class CheckList : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CheckListId { get; set; }

        [DataMember]
        public double ACTUAL { get; set; }

        [DataMember]
        public string SOURCE { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string CAPTION { get; set; }

        
        public int EntityId
        {
            get
            {
                return CheckListId;
            }
        }

    }
}
