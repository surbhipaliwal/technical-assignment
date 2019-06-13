using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public static class JsonStreamDeserializer
    {
        /// <summary>
        /// Reading the json as a stream in case of success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        /// <summary>
        /// Reading the json as a stream in case the request failed
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
