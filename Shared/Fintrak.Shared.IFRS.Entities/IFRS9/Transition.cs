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
    public partial class Transition : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int TransitionId { get; set; }

        [DataMember]
        public string Prudential_Classification { get; set; }


        [DataMember]
        public int PDD_LowerBoundary { get; set; }

        [DataMember]
        public int PDD_UpperBoundary { get; set; }


        [DataMember]
        public string IFRS9_Classification { get; set; }
               

        //[DataMember]
        //public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return TransitionId;
            }
        }
    }
}
