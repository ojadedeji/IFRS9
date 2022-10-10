using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ISubSectorRepository : IDataRepository<SubSector>
    {
        IEnumerable<SubSector> GetSubSectorBySource(string Source);
        IEnumerable<SubSectorInfo> GetSubSectors(string Source);
        IEnumerable<SubSectorInfo> GetSubSectorsBySectorCode(string Source, string sectorCode);
    }
}
