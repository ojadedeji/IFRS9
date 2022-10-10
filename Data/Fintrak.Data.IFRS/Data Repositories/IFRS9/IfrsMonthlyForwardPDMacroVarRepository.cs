using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsMonthlyForwardPDMacroVarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsMonthlyForwardPDMacroVarRepository : DataRepositoryBase<IfrsMonthlyForwardPDMacroVar>, IIfrsMonthlyForwardPDMacroVarRepository
    {
        protected override IfrsMonthlyForwardPDMacroVar AddEntity(IFRSContext entityContext, IfrsMonthlyForwardPDMacroVar entity)
        {
            return entityContext.Set<IfrsMonthlyForwardPDMacroVar>().Add(entity);
        }

        protected override IfrsMonthlyForwardPDMacroVar UpdateEntity(IFRSContext entityContext, IfrsMonthlyForwardPDMacroVar entity)
        {
            return (from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsMonthlyForwardPDMacroVar> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>()
                   select e;
        }

        protected override IfrsMonthlyForwardPDMacroVar GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsMonthlyForwardPDMacroVar> GetAllIfrsMonthlyForwardPDMacroVar(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.sub_type,
                                     Scenario = e.Scenerio,
                                     Month_1 = e._1,
                                     Month_2 = e._2,
                                     Month_3 = e._3,
                                     Month_4 = e._4,
                                     Month_5 = e._5,
                                     Month_6 = e._6,
                                     Month_7 = e._7,
                                     Month_8 = e._8,
                                     Month_9 = e._9,
                                     Month_10 = e._10,
                                     Month_11 = e._11,
                                     Month_12 = e._12,
                                     Month_13 = e._13,
                                     Month_14 = e._14,
                                     Month_15 = e._15,
                                     Month_16 = e._16,
                                     Month_17 = e._17,
                                     Month_18 = e._18,
                                     Month_19 = e._19,
                                     Month_20 = e._20,
                                     Month_21 = e._21,
                                     Month_22 = e._22,
                                     Month_23 = e._23,
                                     Month_24 = e._24,
                                     Month_25 = e._25,
                                     Month_26 = e._26,
                                     Month_27 = e._27,
                                     Month_28 = e._28,
                                     Month_29 = e._29,
                                     Month_30 = e._30,
                                     Month_31 = e._31,
                                     Month_32 = e._32,
                                     Month_33 = e._33,
                                     Month_34 = e._34,
                                     Month_35 = e._35,
                                     Month_36 = e._36,
                                     Month_37 = e._37,
                                     Month_38 = e._38,
                                     Month_39 = e._39,
                                     Month_40 = e._40,
                                     Month_41 = e._41,
                                     Month_42 = e._42,
                                     Month_43 = e._43,
                                     Month_44 = e._44,
                                     Month_45 = e._45,
                                     Month_46 = e._46,
                                     Month_47 = e._47,
                                     Month_48 = e._48,
                                     Month_49 = e._49,
                                     Month_50 = e._50,
                                     Month_51 = e._51,
                                     Month_52 = e._52,
                                     Month_53 = e._53,
                                     Month_54 = e._54,
                                     Month_55 = e._55,
                                     Month_56 = e._56,
                                     Month_57 = e._57,
                                     Month_58 = e._58,
                                     Month_59 = e._59,
                                     Month_60 = e._60,
                                     Month_61 = e._61,
                                     Month_62 = e._62,
                                     Month_63 = e._63,
                                     Month_64 = e._64,
                                     Month_65 = e._65,
                                     Month_66 = e._66,
                                     Month_67 = e._67,
                                     Month_68 = e._68,
                                     Month_69 = e._69,
                                     Month_70 = e._70,
                                     Month_71 = e._71,
                                     Month_72 = e._72,
                                     Month_73 = e._73,
                                     Month_74 = e._74,
                                     Month_75 = e._75,
                                     Month_76 = e._76,
                                     Month_77 = e._77,
                                     Month_78 = e._78,
                                     Month_79 = e._79,
                                     Month_80 = e._80,
                                     Month_81 = e._81,
                                     Month_82 = e._82,
                                     Month_83 = e._83,
                                     Month_84 = e._84,
                                     Month_85 = e._85,
                                     Month_86 = e._86,
                                     Month_87 = e._87,
                                     Month_88 = e._88,
                                     Month_89 = e._89,
                                     Month_90 = e._90,
                                     Month_91 = e._91,
                                     Month_92 = e._92,
                                     Month_93 = e._93,
                                     Month_94 = e._94,
                                     Month_95 = e._95,
                                     Month_96 = e._96,
                                     Month_97 = e._97,
                                     Month_98 = e._98,
                                     Month_99 = e._99,
                                     Month_100 = e._100,
                                     Month_101 = e._101,
                                     Month_102 = e._102,
                                     Month_103 = e._103,
                                     Month_104 = e._104,
                                     Month_105 = e._105,
                                     Month_106 = e._106,
                                     Month_107 = e._107,
                                     Month_108 = e._108,
                                     Month_109 = e._109,
                                     Month_110 = e._110,
                                     Month_111 = e._111,
                                     Month_112 = e._112,
                                     Month_113 = e._113,
                                     Month_114 = e._114,
                                     Month_115 = e._115,
                                     Month_116 = e._116,
                                     Month_117 = e._117,
                                     Month_118 = e._118,
                                     Month_119 = e._119,
                                     Month_120 = e._120,
                                     Month_121 = e._121,
                                     Month_122 = e._122,
                                     Month_123 = e._123,
                                     Month_124 = e._124,
                                     Month_125 = e._125,
                                     Month_126 = e._126,
                                     Month_127 = e._127,
                                     Month_128 = e._128,
                                     Month_129 = e._129,
                                     Month_130 = e._130,
                                     Month_131 = e._131,
                                     Month_132 = e._132
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.Scenario);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsMonthlyForwardPDMacroVar>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Scenerio).ThenByDescending(c => c.Col);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMonthlyForwardPDMacroVar>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }


    }
}
