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
    public partial class CDQMAddress : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int AddressId { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string LGA { get; set; }

        [DataMember]
        public string State { get; set; }

      
        public int EntityId
        {
            get
            {
                return AddressId;
            }
        }
    }
}
