using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHistoricalSectorialLGDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalSectorialLGDRepository : DataRepositoryBase<HistoricalSectorialLGD>, IHistoricalSectorialLGDRepository
    {
        protected override HistoricalSectorialLGD AddEntity(IFRSContext entityContext, HistoricalSectorialLGD entity)
        {
            return entityContext.Set<HistoricalSectorialLGD>().Add(entity);
        }

        protected override HistoricalSectorialLGD UpdateEntity(IFRSContext entityContext, HistoricalSectorialLGD entity)
        {
            return (from e in entityContext.Set<HistoricalSectorialLGD>()
                    where e.HistoricalSectorialLGDId == entity.HistoricalSectorialLGDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalSectorialLGD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalSectorialLGD>()
                   select e;
        }

        protected override HistoricalSectorialLGD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalSectorialLGD>()
                         where e.HistoricalSectorialLGDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}