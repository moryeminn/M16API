using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M16AlignAPI.Models
{
    public class MissionsDatabaseSettings : IMissionsDatabaseSettings
    {
        public string MissionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMissionsDatabaseSettings
    {
        string MissionsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}