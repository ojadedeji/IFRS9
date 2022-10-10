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

namespace Fintrak.Shared.MPR.Entities
{
    public partial class MessagingSubscription : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int MessagingSubscriptionId { get; set; }

        [DataMember]
        [Required]
        public string Recipents { get; set; }

        [DataMember]
        [Required]
        public string Subjects { get; set; }

        [DataMember]
        [Required]
        public string eMessage { get; set; }

        [DataMember]
        public string Report { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        [Required]
        public string FileType { get; set; }

        [DataMember]
        public bool TriggeredBy { get; set; }

        [DataMember]
        public string ReportID { get; set; }

        public bool Active { get; set; }


        public int EntityId
        {
            get
            {
                return MessagingSubscriptionId;
            }
        }
    }
}
