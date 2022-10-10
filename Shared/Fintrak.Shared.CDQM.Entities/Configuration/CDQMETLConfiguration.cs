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
    public partial class CDQMETLConfiguration : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ETLConfigurationId { get; set; }

        [DataMember]
        public string ConfigurationFilter { get; set; }

        [DataMember]
        public string ConfigurationValue { get; set; }

        [DataMember]
        public string PackagePath { get; set; }

        [DataMember]
        public string ConfiguredValueType { get; set; }

   

      
        public int EntityId
        {
            get
            {
                return ETLConfigurationId;
            }
        }
    }
}
