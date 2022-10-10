using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Fintrak.Shared.Common.Core
{
    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject
    {
        public EntityBase()
        {
            Active = true;
            Deleted = false;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string UpdatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedOn { get; set; }

        [XmlIgnore]
        [DataMember]
        public byte[] RowVersion { get; set; }

        #region IExtensibleDataObject Members

        [XmlIgnore]
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }
}
