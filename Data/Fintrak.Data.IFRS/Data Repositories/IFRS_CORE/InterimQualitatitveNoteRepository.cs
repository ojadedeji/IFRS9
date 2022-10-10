using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInterimQualitativeNoteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InterimQualitativeNoteRepository : DataRepositoryBase<InterimQualitativeNote>, IInterimQualitativeNoteRepository
    {

        protected override InterimQualitativeNote AddEntity(IFRSContext entityContext, InterimQualitativeNote entity)
        {
            return entityContext.Set<InterimQualitativeNote>().Add(entity);
        }

        protected override InterimQualitativeNote UpdateEntity(IFRSContext entityContext, InterimQualitativeNote entity)
        {
            return (from e in entityContext.Set<InterimQualitativeNote>() 
                    where e.InterimQualitativeNoteId == entity.InterimQualitativeNoteId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<InterimQualitativeNote> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<InterimQualitativeNote>()
                   select e;
        }

        protected override InterimQualitativeNote GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InterimQualitativeNote>()
                         where e.InterimQualitativeNoteId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
