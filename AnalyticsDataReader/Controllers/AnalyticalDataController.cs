using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyticsDataReader.DAL.Model;
using AnalyticsDataReader.Domain.Queries.Data;
using AnalyticsDataReader.Domain.Commands.Data;
using AnalyticsDataReader.Domain.Models;
using AnalyticsDataReader.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using AnalyticsDataReader.Models;
using Microsoft.Extensions.Configuration;

namespace AnalyticsDataReader.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticalDataController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AnalyticalDataController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        /// <summary>
        /// Get all Analytical Data from the API. 
        /// </summary>
        /// <returns>List of all Analytical Data.</returns>
        [HttpGet("GetAnalyticalDataAll")]
        [ProducesResponseType(typeof(IEnumerable<AnalyticalDataPoint>), 200)]
        public async Task<IEnumerable<AnalyticalDataPoint>> GetAnalyticalDataAll()
        {
            return await _mediator.Send(new GetDataAllQuery());
        }

        [HttpGet("GetAnalyticalMetaDataAll")]
        [ProducesResponseType(typeof(IEnumerable<AnalyticalMetaData>), 200)]
        public async Task<IEnumerable<AnalyticalMetaData>> GetAnalyticalMetaDataAll()
        {
            return await _mediator.Send(new GetMetaDataAllQuery());
        }

        /// <summary>
        /// Lists a given user by their ID.
        /// </summary>
        /// <param name="startDate">startDate</param>
        /// <param name="endDate">endDate</param>
        /// <returns>User.</returns>
        [HttpGet("GetAnalyticalDataRange/startDate={startDate}&endDate={endDate}", Name = nameof(GetAnalyticalDataRange))]
        [ProducesResponseType(typeof(AnalyticalDataPoint), 200)]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<AnalyticalDataPoint>> GetAnalyticalDataRange(DateTime startDate, DateTime endDate)
        {
            return await _mediator.Send(new GetDataRangeQuery(startDate, endDate)); ;
        }

        /// <summary>
        /// Add new DataSet
        /// </summary>
        /// <param name="fileToImport">fileToImport</param>
        /// <returns>Upload results</returns>
        [HttpPost("ImportNewDataSet/")]//fileToImport={fileToImport}

        public async Task<Response<FileLoadResults>> ImportNewDataSet(string fileToImport)
        {
            fileToImport = _configuration.GetSection("LocalUploadPath").Value + "sampleSheet.csv";
            return await _mediator.Send(new UploadDataCommand(fileToImport));
        }
    }
}
