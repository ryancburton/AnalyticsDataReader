using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnalyticsDataReader.DAL.Model
{
    public class AnalyticalMetaData
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime DateOfUpload { get; set; }

        [Required]
        public decimal MinForSeries { get; set; }
        [Required]
        public decimal MaxForSeries { get; set; }
        [Required]
        public decimal AverageForSeries { get; set; }
        [Required]
        public DateTime StartOfMostExpensiveHour { get; set; }
        [Required]
        public long SeriesStartID { get; set; }
        [Required]
        public long SeriesEndID { get; set; }
    }
}