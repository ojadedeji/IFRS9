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
    public partial class CDQMCountry : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CountryId { get; set; }

        [DataMember]
        public string Valid { get; set; }

        [DataMember]
        public string Invalid { get; set; }

       

      
        public int EntityId
        {
            get
            {
                return CountryId;
            }
        }
    }
}
