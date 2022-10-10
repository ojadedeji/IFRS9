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
    public partial class IfrsMarginalPDByScenerio : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int   ID { get; set; }

        [DataMember]
        [Required]
        public string ProductType  { get; set; }

        [DataMember]
        public string sub_type { get; set; }

        [DataMember]
       public string Scenerio { get; set; }

        [DataMember]
        public int fnyr { get; set; }


    [DataMember]
    public double _1 { get; set; }

   [DataMember]
    public double _2 { get; set; }


    public double _3 { get; set; }


    [DataMember]
    public double _4 { get; set; }

    [DataMember]
    public double _5 { get; set; }

    [DataMember]
    public double _6 { get; set; }
    [DataMember]
    public double _7 { get; set; }

    [DataMember]
    public double _8 { get; set; }

    [DataMember]
    public double _9 { get; set; }

    [DataMember]
    public double _10 { get; set; }

    [DataMember]
    public double _11 { get; set; }

    [DataMember]
    public double _12 { get; set; }

    [DataMember]
    public double _13 { get; set; }

    [DataMember]
    public double _14 { get; set; }

    [DataMember]
    public double _15 { get; set; }

    [DataMember]
    public double _16 { get; set; }

    [DataMember]
    public double _17 { get; set; }

    [DataMember]
    public double _18 { get; set; }

    [DataMember]
    public double _19 { get; set; }

    [DataMember]
    public double _20 { get; set; }

    [DataMember]
    public double _21 { get; set; }

    [DataMember]
    public double _22 { get; set; }

    [DataMember]
    public double _23 { get; set; }

    [DataMember]
    public double _24 { get; set; }


    [DataMember]
    public double _25 { get; set; }


    [DataMember]
    public double _26 { get; set; }

    [DataMember]
    public double _27 { get; set; }

    [DataMember]
    public double _28 { get; set; }

    [DataMember]
    public double _29 { get; set; }

    [DataMember]
    public double _30 { get; set; }

    [DataMember]
    public double _31 { get; set; }

    [DataMember]
    public double _32 { get; set; }

    [DataMember]
    public double _33 { get; set; }

    [DataMember]
    public double _34 { get; set; }

    [DataMember]
    public double _35 { get; set; }

    [DataMember]
    public double _36 { get; set; }

    [DataMember]
    public double _37 { get; set; }

    [DataMember]
    public double _38 { get; set; }

    [DataMember]
    public double _39 { get; set; }

    [DataMember]
    public double _40 { get; set; }

    [DataMember]
    public double _41 { get; set; }

    [DataMember]
    public double _42  { get; set; }
    [DataMember]
    public double _43 { get; set; }

    [DataMember]
    public double _44 { get; set; }

    [DataMember]
    public double _45 { get; set; }

    [DataMember]
    public double _46 { get; set; }


    [DataMember]
    public double _47 { get; set; }


    [DataMember]
    public double _48 { get; set; }



    [DataMember]
    public double _49 { get; set; }



    [DataMember]
    public double _50 { get; set; }



    [DataMember]
    public double _51 { get; set; }

    [DataMember]
    public double _52 { get; set; }

    [DataMember]
    public double _53 { get; set; }

    [DataMember]
    public double _54 { get; set; }

    [DataMember]
    public double _55 { get; set; }


    [DataMember]
    public double _56 { get; set; }



    [DataMember]
    public double _57 { get; set; }


    [DataMember]
    public double _58 { get; set; }


    [DataMember]
    public double _59 { get; set; }


    [DataMember]
    public double _60 { get; set; }


    [DataMember]
    public double _61 { get; set; }


    [DataMember]
    public double _62 { get; set; }

    [DataMember]
    public double _63 { get; set; }

    [DataMember]
    public double _64 { get; set; }

    [DataMember]
    public double _65 { get; set; }

    [DataMember]
    public double _66 { get; set; }

    [DataMember]
    public double _67 { get; set; }

    [DataMember]
    public double _68 { get; set; }

    [DataMember]
    public double _69 { get; set; }

    [DataMember]
    public double _70 { get; set; }

    [DataMember]
    public double _71 { get; set; }

    [DataMember]
    public double _72 { get; set; }

    [DataMember]
    public double _73 { get; set; }

    [DataMember]
    public double _74 { get; set; }

    [DataMember]
    public double _75 { get; set; }

    [DataMember]
    public double _76 { get; set; }


    [DataMember]
    public double _77 { get; set; }


    [DataMember]
    public double _78 { get; set; }


    [DataMember]
    public double _79 { get; set; }


    [DataMember]
    public double _80 { get; set; }


    [DataMember]
    public double _81 { get; set; }


    [DataMember]
    public double _82 { get; set; }


    [DataMember]
    public double _83 { get; set; }


    [DataMember]
    public double _84 { get; set; }

    [DataMember]
    public double _85 { get; set; }

    [DataMember]
    public double _86 { get; set; }

    [DataMember]
    public double _87 { get; set; }

    [DataMember]
    public double _88 { get; set; }


    [DataMember]
    public double _89 { get; set; }


    [DataMember]
    public double _90 { get; set; }


    [DataMember]
    public double _91  { get; set; }


    [DataMember]
    public double _92 { get; set; }


    [DataMember]
    public double _93 { get; set; }


    [DataMember]
    public double _94 { get; set; }


    [DataMember]
    public double _95 { get; set; }

    [DataMember]
    public double _96 { get; set; }

    [DataMember]
    public double _97 { get; set; }

    [DataMember]
    public double _98 { get; set; }

    [DataMember]
    public double _99 { get; set; }

    [DataMember]
    public double _100 { get; set; }

    [DataMember]
    public double _101 { get; set; }

    [DataMember]
    public double _102 { get; set; }

    [DataMember]
    public double _103 { get; set; }

    [DataMember]
    public double _104 { get; set; }


    [DataMember]
    public double _105 { get; set; }


    [DataMember]
    public double _106 { get; set; }

    [DataMember]
    public double _107 { get; set; }
    [DataMember]
    public double _108 { get; set; }
   
    [DataMember]
    public double _109 { get; set; }
    [DataMember]
    public double _110 { get; set; }

    [DataMember]
    public double _111 { get; set; }

    [DataMember]
    public double _112 { get; set; }

    [DataMember]
    public double _113 { get; set; }

    [DataMember]
    public double _114 { get; set; }

    [DataMember]
    public double _115 { get; set; }

    [DataMember]
    public double _116 { get; set; }

    [DataMember]
    public double _117 { get; set; }


    [DataMember]
    public double _118 { get; set; }


    [DataMember]
    public double _119 { get; set; }

    [DataMember]
    public double _120 { get; set; }

    [DataMember]
    public double _121 { get; set; }

    [DataMember]
    public double _122 { get; set; }


    [DataMember]
    public double _123 { get; set; }

    [DataMember]
    public double _124 { get; set; }

    [DataMember]
    public double _125 { get; set; }

    [DataMember]
    public double _126 { get; set; }

    [DataMember]
    public double _127 { get; set; }

    [DataMember]
    public double _128 { get; set; }

    [DataMember]
    public double _129 { get; set; }

    [DataMember]
    public double _130 { get; set; }

    [DataMember]
    public double _131 { get; set; }

    [DataMember]
    public double _132 { get; set; }

  



















    //[DataMember]
    //public string CompanyCode { get; set; }


    [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return  ID;
            }
        }
    }
}
