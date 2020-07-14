using System;
using System.ComponentModel.DataAnnotations;

namespace AnalyticsDataReader.DAL.Model
{
    public class AnalyticalDataPoint
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public decimal point { get; set; }
    }
}
