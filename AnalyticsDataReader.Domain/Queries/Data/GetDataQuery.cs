using System;
using System.Collections.Generic;
using AnalyticsDataReader.DAL.Model;
using MediatR;

namespace AnalyticsDataReader.Domain.Queries.Data
{
    public class GetDataAllQuery : IRequest<IEnumerable<AnalyticalDataPoint>>
    {
        public GetDataAllQuery()
        {
        }
    }

    public class GetDataRangeQuery : IRequest<IEnumerable<AnalyticalDataPoint>>
    {
        public DateTime _startDate { get; private set; }
        public DateTime _endDate { get; private set; }

        public GetDataRangeQuery(DateTime startDate, DateTime endDate)
        {
            startDate = _startDate;
            endDate = _endDate;

        }
    }

    public class GetMetaDataAllQuery : IRequest<IEnumerable<AnalyticalMetaData>>
    {
        public GetMetaDataAllQuery()
        {
        }
    }
}
