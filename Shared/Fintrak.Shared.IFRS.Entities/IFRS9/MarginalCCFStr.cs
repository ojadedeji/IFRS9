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
    public partial class MarginalCCFStr: EntityBase, IIdentifiableEntity
    {
           
        /*
         Id	int	Unchecked
         seq	int	Checked
         OBEType	varchar(50)	Checked
         MonthlyCCF	float	Checked
         MarginalCCF	float	Checked
        */


        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public int seq { get; set; }

        [DataMember]
        public string OBEType { get; set; }

        [DataMember]
        public double MonthlyCCF { get; set; }

        [DataMember]
        public double MarginalCCF { get; set; }




        [DataMember]
        public bool Active { get; set; }


        public int EntityId {
            get {
                return Id;
            }
        }

    }
}
