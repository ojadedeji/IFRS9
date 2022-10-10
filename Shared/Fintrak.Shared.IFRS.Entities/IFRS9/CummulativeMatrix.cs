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
    public partial class CummulativeMatrix : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Mat_level { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public double Stage1 { get; set; }

        [DataMember]
        public double Stage2 { get; set; }

        [DataMember]
        public double Stage3 { get; set; }

        [DataMember]
        public DateTime Quater { get; set; }

        [DataMember]
        public string Scenerio { get; set; }


        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
