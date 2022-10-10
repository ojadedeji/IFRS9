using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsMarginalPDByScenerioRepository : IDataRepository<IfrsMarginalPDByScenerio>
    {
        //IEnumerable<IfrsMarginalPDByScenerio> GetIfrsMarginalPDByScenerioBySource(string Source);
        IEnumerable<IfrsMarginalPDByScenerio> GetAllIfrsMarginalPDByScenerio(int defaultCount, string path);
    }
}
