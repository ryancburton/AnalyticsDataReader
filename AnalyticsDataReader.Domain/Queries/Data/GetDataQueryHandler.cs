using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsDataReader.DAL.Model;
using AnalyticsDataReader.DAL.Repository;
using MediatR;
using System.Collections.Generic;

namespace AnalyticsDataReader.Domain.Queries.Data
{
    public class GetDataAllQueryHandler : IRequestHandler<GetDataAllQuery, IEnumerable<AnalyticalDataPoint>>
    {
        private readonly IAnalyticalDataRepository _analyticalDataRepository;

        public GetDataAllQueryHandler(IAnalyticalDataRepository analyticalDataRepository)
        {
            _analyticalDataRepository = analyticalDataRepository;
        }

        public async Task<IEnumerable<AnalyticalDataPoint>> Handle (GetDataAllQuery request, CancellationToken cancellationToken)
        {
            return await _analyticalDataRepository.GetDataAllAsync();
        }
    }

    public class GetDataRangeQueryHandler : IRequestHandler<GetDataRangeQuery, IEnumerable<AnalyticalDataPoint>>
    {
        private readonly IAnalyticalDataRepository _analyticalDataRepository;

        public GetDataRangeQueryHandler(IAnalyticalDataRepository analyticalDataRepository)
        {
            _analyticalDataRepository = analyticalDataRepository;
        }

        public async Task<IEnumerable<AnalyticalDataPoint>> Handle (GetDataRangeQuery request, CancellationToken cancellationToken)
        {
            if (request._startDate >= request._startDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            return await _analyticalDataRepository.GetDataRangeAsync(request._startDate, request._endDate);
        }
    }

    public class GetMetaDataQueryHandler : IRequestHandler<GetMetaDataAllQuery, IEnumerable<AnalyticalMetaData>>
    {
        private readonly IAnalyticalMetaDataRepository _analyticalMetaDataRepository;

        public GetMetaDataQueryHandler(IAnalyticalMetaDataRepository analyticalMeataDataRepository)
        {
            _analyticalMetaDataRepository = analyticalMeataDataRepository;
        }

        public async Task<IEnumerable<AnalyticalMetaData>> Handle(GetMetaDataAllQuery request, CancellationToken cancellationToken)
        {
            return await _analyticalMetaDataRepository.GetDataAllAsync();
        }
    }
}
