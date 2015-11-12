using Microsoft.Azure.WebJobs;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using JobHost = Microsoft.Azure.WebJobs.JobHost;

namespace WebJobResize
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        static void Main()
        {
            JobHost host = new JobHost();
            host.RunAndBlock();
        }
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        public static void Resize(
       [BlobTrigger(@"images-input/{name}")] WebImage input,
       [Blob(@"images2-output/{name}")] out WebImage output)
        {
            var width = 300;
            var height = 300;
            output = input.Resize(width, height);
        }
        public static void WaterMark(
        [BlobTrigger(@"images2-output/{name}")] WebImage input,
        [Blob(@"images2-newoutput/{name}")] out WebImage output)
        {
            output = input.AddTextWatermark("WebJobs is now awesome!!!!", fontSize: 6);
        }
    }
    public class WebImageBinder : ICloudBlobStreamBinder<WebImage>
    {
        public Task<WebImage> ReadFromStreamAsync(Stream input, CancellationToken cancellationToken)
        {
            return Task.FromResult(new WebImage(input));
        }

        public async Task WriteToStreamAsync(WebImage result, Stream output, CancellationToken cancellationToken)
        {
            var bytes = result.GetBytes();
            await output.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        }
    }
}