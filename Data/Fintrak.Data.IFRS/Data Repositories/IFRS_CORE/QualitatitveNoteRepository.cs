using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IQualitativeNoteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QualitativeNoteRepository : DataRepositoryBase<QualitativeNote>, IQualitativeNoteRepository
    {

        protected override QualitativeNote AddEntity(IFRSContext entityContext, QualitativeNote entity)
        {
            return entityContext.Set<QualitativeNote>().Add(entity);
        }

        protected override QualitativeNote UpdateEntity(IFRSContext entityContext, QualitativeNote entity)
        {
            return (from e in entityContext.Set<QualitativeNote>() 
                    where e.QualitativeNoteId == entity.QualitativeNoteId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<QualitativeNote> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<QualitativeNote>()
                   select e;
        }

        protected override QualitativeNote GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<QualitativeNote>()
                         where e.QualitativeNoteId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
