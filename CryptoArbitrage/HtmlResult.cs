using System.IO;
using System.Xml.Serialization;

namespace CryptoArbitrage
{
    public class HtmlResult : IResult
    {
        // Create the serializer that will actually perform the XML serialization
        

        // The object to serialize
        private readonly string result;

        public HtmlResult(string result)
        {
           this.result = result; 
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            // NOTE: best practice would be to pull this, we'll look at this shortly
            using var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(result);
            writer.Flush();
            // Serialize the object synchronously then rewind the stream
            //Serializer.Serialize(ms, result);
            ms.Position = 0;

            httpContext.Response.ContentType = "text/html";
            await ms.CopyToAsync(httpContext.Response.Body);
        }
    }
}
