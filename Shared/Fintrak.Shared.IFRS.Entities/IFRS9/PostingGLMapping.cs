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

namespace Fintrak.Shared.IFRS.Entities{

    public partial class PostingGLMapping : EntityBase, IIdentifiableEntity {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember] public string BS_GL { get; set; }
        [DataMember] public string PL_GL { get; set; }
        [DataMember] public string BS_Description { get; set; }
        [DataMember] public string PL_Description { get; set; }
        [DataMember] public string ProductCategory { get; set; }
        [DataMember] public string Classification { get; set; }

        public bool Active { get; set; }

        public int EntityId{
            get{
                return ID;
            }
        }
    }
}
