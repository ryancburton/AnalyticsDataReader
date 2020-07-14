using System.ComponentModel.DataAnnotations;

namespace AnalyticsDataReader.Models
{
    public class AddAnalyticsData
    {
        [Required]
        public string filePath { get; set; }
    }
}
