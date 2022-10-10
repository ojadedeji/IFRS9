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
    public partial class QualitativeNote : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int QualitativeNoteId { get; set; }

        [DataMember]
        [Required]
        public string RefNote { get; set; }

        [DataMember]
     
        public string TopNotes { get; set; }

        [DataMember]
     
        public string BottomNotes { get; set; }

        [DataMember]
       
        public int ReportType { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]    
        public string DisplayName { get; set; }

        public int EntityId
        {
            get
            {
                return QualitativeNoteId;
            }
        }
    }
}
