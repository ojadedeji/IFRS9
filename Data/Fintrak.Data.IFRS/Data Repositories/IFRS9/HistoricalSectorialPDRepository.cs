using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHistoricalSectorialPDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalSectorialPDRepository : DataRepositoryBase<HistoricalSectorialPD>, IHistoricalSectorialPDRepository
    {
        protected override HistoricalSectorialPD AddEntity(IFRSContext entityContext, HistoricalSectorialPD entity)
        {
            return entityContext.Set<HistoricalSectorialPD>().Add(entity);
        }

        protected override HistoricalSectorialPD UpdateEntity(IFRSContext entityContext, HistoricalSectorialPD entity)
        {
            return (from e in entityContext.Set<HistoricalSectorialPD>()
                    where e.HistoricalSectorialPDId == entity.HistoricalSectorialPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalSectorialPD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalSectorialPD>()
                   select e;
        }

        protected override HistoricalSectorialPD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalSectorialPD>()
                         where e.HistoricalSectorialPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}