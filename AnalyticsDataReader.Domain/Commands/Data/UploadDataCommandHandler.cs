using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsDataReader.Domain.Response;
using AnalyticsDataReader.Domain.Models;
using AnalyticsDataReader.DAL.Repository;
using AnalyticsDataReader.DAL.Model;
using MediatR;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace AnalyticsDataReader.Domain.Commands.Data
{
    public class UploadDataCommandHandler : IRequestHandler<UploadDataCommand, Response<FileLoadResults>>
    {
        private readonly IAnalyticalDataRepository _analyticalDataRepository;
        private readonly IAnalyticalMetaDataRepository _analyticalMetaDataRepository;
        private List<AnalyticalDataPoint> _points = new List<AnalyticalDataPoint>();

        public UploadDataCommandHandler(IAnalyticalDataRepository analyticalDataRepository, IAnalyticalMetaDataRepository analyticalMetaDataRepository)
        {
            _analyticalDataRepository = analyticalDataRepository;
            _analyticalMetaDataRepository = analyticalMetaDataRepository;
        }

        public async Task<Response<FileLoadResults>> Handle(UploadDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                FileLoadResults fileLoadResults = new FileLoadResults();

                if (!File.Exists(request._filePath))
                {
                    fileLoadResults.fileLoadSuccess = false;
                    fileLoadResults.loadingError = "File does not exist.";
                    throw new Exception("File does not exist.");
                }
                else
                {
                    int counter = 0;
                    string dataPoint;
                    System.IO.StreamReader file = new System.IO.StreamReader(request._filePath);

                    while ((dataPoint = file.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            //Skip line as expected to be Data label
                            counter++;
                            continue;
                        }

                        //TODo Move this logic into a seperate class
                        String[] data = dataPoint.Split(',');
                        string dateFormat = "dd/MM/yyyy HH:mm";
                        DateTime parsedDate;
                        decimal point = 0;

                        if (data[0].Length < 16)
                        {
                            data[0] = data[0] + " 00:00";
                        }

                        if (!DateTime.TryParseExact(data[0], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            fileLoadResults.fileLoadSuccess = false;
                            fileLoadResults.loadingError = "Date in incorrect format. Should be dd/mm/YYYY HH:MM.";
                            throw new Exception("Date in incorrect format. Should be dd/mm/YYYY HH:MM.");
                        }

                        if (data[1].Contains("."))
                        {
                            data[1] = data[1].Replace(".", ",");
                        }

                        if (!decimal.TryParse(data[1], out point))
                        {
                            fileLoadResults.fileLoadSuccess = false;
                            fileLoadResults.loadingError = "Point in incorrect decimal format.";
                            throw new Exception("Point in incorrect decimal format.");
                        }

                        AnalyticalDataPoint analyticalDataPoint = new AnalyticalDataPoint
                        {
                            DateTime = parsedDate,
                            point = point
                        };

                        await _analyticalDataRepository.AddNewEntity(analyticalDataPoint);
                        _points.Add(analyticalDataPoint);
                        counter++;
                    }

                    fileLoadResults.fileLoadSuccess = true;
                    CalculateAnalytics();
                }

                return new Response<FileLoadResults>(true, "Data succesfully loaded.", fileLoadResults);
            }
            catch (Exception ex)
            {
                FileLoadResults fileLoadResults = new FileLoadResults();
                fileLoadResults.fileLoadSuccess = false;
                fileLoadResults.loadingError = ex.Message;
                return new Response<FileLoadResults>(false, ex.Message, fileLoadResults);
            }
        }

        public void CalculateAnalytics()
        {
            var min = _points.Min(a => a.point);
            var max = _points.Max(a => a.point);
            var average = _points.Average(a => a.point);
            var ExpWindow = _points.Min(a => a.point);

            var maxHour = _points.GroupBy(q => new
                                        {
                                            Year = q.DateTime.Year,
                                            Month = q.DateTime.Month,
                                            Day = q.DateTime.Day,
                                            Hour = q.DateTime.Hour

                                        })
                                .Select(s => new { Date = s.Key, MaxPoints = s.Sum(c => c.point) })
                                .OrderByDescending(group => group.MaxPoints).First();

            AnalyticalMetaData analyticalMetaData = new AnalyticalMetaData
            {
                DateOfUpload = DateTime.Now,
                MinForSeries = min,
                MaxForSeries = max,
                AverageForSeries = average,
                StartOfMostExpensiveHour = new DateTime(maxHour.Date.Year, maxHour.Date.Month, maxHour.Date.Day, maxHour.Date.Hour, 0, 0),
                SeriesStartID = _points[0].Id,
                SeriesEndID = _points[_points.Count - 1].Id
            };

            _analyticalMetaDataRepository.AddNewEntity(analyticalMetaData);
        }
    }
}