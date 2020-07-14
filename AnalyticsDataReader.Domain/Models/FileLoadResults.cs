using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsDataReader.Domain.Models
{
    public class FileLoadResults
    {
        public bool fileLoadSuccess { get; set; }

        public int pointsLoaded { get; set; }

        public string loadingError { get; set; }
    }
}
