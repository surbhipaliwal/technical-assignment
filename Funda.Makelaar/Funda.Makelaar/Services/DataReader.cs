using Funda.Makelaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public class DataReader : IDataReader
    {
        private HttpClient _client;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public DataReader(HttpClient http)
        {
            _client = http;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// Get the list of houses registered on funda to buy
        /// </summary>
        /// <returns>Returns all the houses listedd on funda to buy</returns>
        private async Task<List<House>> FetchAllListings(string basicUrl, CancellationToken cancellationToken)
        {
            var listings = new List<House>();
            Listing result = null;
            int currentPage = 1, totalPages = 0;
                    do
                    {
                        string url = $"{basicUrl}/&page={currentPage}&pagesize=25";
                        var request = new HttpRequestMessage(HttpMethod.Get, url);
                        var response = _client.SendAsync(request, cancellationToken).Result;
                        Thread.Sleep(TimeSpan.FromMilliseconds(600));
                        var stream = await response.Content.ReadAsStreamAsync();

                        //Throwing an exception if the request did not go through
                        if (response.IsSuccessStatusCode == false)
                        {
                            var content = await JsonStreamDeserializer.StreamToStringAsync(stream);
                            throw new ApiException
                            {
                                StatusCode = (int)response.StatusCode,
                                Content = content
                            };
                        }

                        //Deserializing json as a stream
                        if (response.IsSuccessStatusCode)
                            result = JsonStreamDeserializer.DeserializeJsonFromStream<Listing>(stream);

                        //Adding all the objects from the current page to the list
                        listings.AddRange(result?.Objects);

                        //Fetching the total number of pages
                        totalPages = result?.Paging?.AantalPaginas ?? 0;

                        //Incrementing the page count to fetch the next page
                        currentPage++;
                    } while (totalPages >= currentPage);
           
            return listings;
        }

        /// <summary>
        /// Get the names of top makelaars
        /// </summary>
        /// <param name="number">Number of makelaars to be fetched</param>
        /// <returns></returns>
        public async Task<List<string>> GetTopMakelaarsFromList(int number, string basicUrl)
        {
            List<string> topMakelaars = new List<string>();
            if(number > 0)
            {
                //Fetching all the houses listed too buy in Amsterdam
                var houseListings = await FetchAllListings(basicUrl, cancellationTokenSource.Token);

                //Grouping the list by makelaarId
                var topList = houseListings.GroupBy(x => x.MakelaarId).OrderByDescending(g => g.Count()).Take(number).ToList();

                //Getting the names of top makelaars with maximum listing
                foreach (var makelaar in topList)
                    topMakelaars.Add(makelaar.First().MakelaarNaam);
            }
            return topMakelaars;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
