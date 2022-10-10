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
    public partial class ifrsexceptionreport : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public double NorminalRate { get; set; }

        [DataMember]
        public double EIR { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }


        [DataMember]
        public string Classification { get; set; }


        [DataMember]
        public string ExceptionType { get; set; }


        
        [DataMember]
        public string ErrorCode { get; set; }


        [DataMember]
        public string ErrDescription { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
