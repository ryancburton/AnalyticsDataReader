using AnalyticsDataReader.Domain.Response;
using AnalyticsDataReader.Domain.Models;
using MediatR;

namespace AnalyticsDataReader.Domain.Commands.Data
{
    public class UploadDataCommand : IRequest<Response<FileLoadResults>>
    {
        public string _filePath { get; set; }

        public UploadDataCommand(string filePath)
        {
            _filePath = filePath;
        }
    }
}
