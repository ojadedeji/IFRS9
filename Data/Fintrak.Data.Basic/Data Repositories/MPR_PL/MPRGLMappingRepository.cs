using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMPRGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRGLMappingRepository : DataRepositoryBase<MPRGLMapping>, IMPRGLMappingRepository
    {

        protected override MPRGLMapping AddEntity(BasicContext entityContext, MPRGLMapping entity)
        {
            return entityContext.Set<MPRGLMapping>().Add(entity);
        }

        protected override MPRGLMapping UpdateEntity(BasicContext entityContext, MPRGLMapping entity)
        {
            return (from e in entityContext.Set<MPRGLMapping>() 
                    where e.MPRGLMappingId == entity.MPRGLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRGLMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MPRGLMapping>()
                   select e;
        }

        protected override MPRGLMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRGLMapping>()
                         where e.MPRGLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public  IEnumerable<MPRGLMappingInfo> GetMPRGLMappings()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.MPRGLMappingSet
                         
                            join c in entityContext.PLCaptionSet on a.CaptionCode equals c.Code into ac
                            from aci in ac.DefaultIfEmpty()
                            join d in entityContext.GLDefinitionSet on a.GLCode equals d.GL_Code into ad
                            from adi in ad.DefaultIfEmpty()
                            select new MPRGLMappingInfo()
                            {
                                MPRGLMapping = a,
                                PLCaption = aci,
                                GLDefinition = adi
               
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}


 //public IEnumerable<ExpenseGLMappingInfo> GetExpenseGLMappings()
 //       {
 //           using (BasicContext entityContext = new BasicContext())
 //           {
 //               var query = from a in entityContext.ExpenseGLMappingSet
 //                           join b in entityContext.ExpenseBasisSet on a.BasisCode equals b.Code into fg
 //                           from fgi in fg.DefaultIfEmpty()
 //                           join c in entityContext.GLDefinitionSet on a.GLCode equals c.GL_Code into fg1
 //                           from fgj in fg1.DefaultIfEmpty()
 //                           select new ExpenseGLMappingInfo()
 //                           {
 //                               ExpenseGLMapping = a,
 //                               ExpenseBasis = fgi,
 //                               GLDefinition = fgj
 //                           };

 //               return query.ToFullyLoaded();
 //           }


