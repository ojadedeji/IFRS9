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
    public partial class   IfrsProjectedCummDefaultFrq : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int   Id { get; set; }

        [DataMember]
        [Required]
        public string ProductType  { get; set; }

        [DataMember]
        public string sub_type { get; set; }

        [DataMember]
        public int  Origin_yr { get; set; }



        [DataMember]
        public double  AdjBal { get; set; }

    [DataMember]
    public double OutBalAfter_yr { get; set; }

    [DataMember]
    public double OutBalAfter_yr1 { get; set; }


    [DataMember]
    public double OutBalAfter_yr2 { get; set; }


    public double OutBalAfter_yr3 { get; set; }


    [DataMember]
    public double OutBalAfter_yr4 { get; set; }

    [DataMember]
    public double OutBalAfter_yr5 { get; set; }

    [DataMember]
    public double OutBalAfter_yr6 { get; set; }
    [DataMember]
    public double OutBalAfter_yr7 { get; set; }

    [DataMember]
    public double OutBalAfter_yr8 { get; set; }

    [DataMember]
    public double OutBalAfter_yr9 { get; set; }

    [DataMember]
    public double OutBalAfter_yr10 { get; set; }

    [DataMember]
    public double OutBalAfter_yr11 { get; set; }

    [DataMember]
    public double OutBalAfter_yr12 { get; set; }

    [DataMember]
    public double OutBalAfter_yr13 { get; set; }

    [DataMember]
    public double OutBalAfter_yr14 { get; set; }

    [DataMember]
    public double OutBalAfter_yr15 { get; set; }

    [DataMember]
    public double OutBalAfter_yr16 { get; set; }

    [DataMember]
    public double OutBalAfter_yr17 { get; set; }

    [DataMember]
    public double OutBalAfter_yr18 { get; set; }

    [DataMember]
    public double OutBalAfter_yr19 { get; set; }

    [DataMember]
    public double OutBalAfter_yr20 { get; set; }

    [DataMember]
    public double OutBalAfter_yr21 { get; set; }

    [DataMember]
    public double OutBalAfter_yr22 { get; set; }

    [DataMember]
    public double OutBalAfter_yr23 { get; set; }

    [DataMember]
    public double OutBalAfter_yr24 { get; set; }


    [DataMember]
    public double OutBalAfter_yr25 { get; set; }


    [DataMember]
    public double OutBalAfter_yr26 { get; set; }

    [DataMember]
    public double OutBalAfter_yr27 { get; set; }

    [DataMember]
    public double OutBalAfter_yr28 { get; set; }

    [DataMember]
    public double OutBalAfter_yr29 { get; set; }

    [DataMember]
    public double OutBalAfter_yr30 { get; set; }

    [DataMember]
    public double OutBalAfter_yr31 { get; set; }

    [DataMember]
    public double OutBalAfter_yr32 { get; set; }

    [DataMember]
    public double OutBalAfter_yr33 { get; set; }

    [DataMember]
    public double OutBalAfter_yr34 { get; set; }

    [DataMember]
    public double OutBalAfter_yr35 { get; set; }

    [DataMember]
    public double OutBalAfter_yr36 { get; set; }

    [DataMember]
    public double OutBalAfter_yr37 { get; set; }

    [DataMember]
    public double OutBalAfter_yr38 { get; set; }

    [DataMember]
    public double OutBalAfter_yr39 { get; set; }

    [DataMember]
    public double OutBalAfter_yr40 { get; set; }






    //[DataMember]
    //public string CompanyCode { get; set; }


    [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return  Id;
            }
        }
    }
}
