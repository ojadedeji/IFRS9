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
    public partial class RatioCaption : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int RatioCaptionID { get; set; }

        [DataMember]
        public string RatioCategory { get; set; }

        [DataMember]
        public string RatioCaptionOpt { get; set; }

        [DataMember]
        public int Position { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return RatioCaptionID;
            }
        }
    }
}
