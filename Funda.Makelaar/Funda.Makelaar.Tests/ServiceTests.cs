using Moq;
using Moq.Protected;
using Ninject;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Funda.Makelaar.Tests
{
    public class ServiceTests
    {
        public StandardKernel Kernel { get; }
        public readonly IDataReader _dataReader;
        public ServiceTests()
        {
            Kernel = new StandardKernel();
            Kernel.Rebind<ILogger>().To<Logger>();
            _dataReader = Kernel.Get<IDataReader>();
        }

        [Fact]
        public void GetObjects_With401Response_ShouldThrowUnAuthorizedException()
        {
            var unauthorizedResponseHttpHandlerMock = new Mock<HttpMessageHandler>();

            unauthorizedResponseHttpHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpResponseMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                });

            var client = new HttpClient(unauthorizedResponseHttpHandlerMock.Object);

            
            
           // var test = _dataReader.
        }
    }
}
