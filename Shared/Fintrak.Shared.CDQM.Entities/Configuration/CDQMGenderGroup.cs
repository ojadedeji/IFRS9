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

namespace Fintrak.Shared.CDQM.Entities
{
    public partial class CDQMGenderGroup : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GenderGroupId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string GroupGender { get; set; }

       

      
        public int EntityId
        {
            get
            {
                return GenderGroupId;
            }
        }
    }
}
