namespace AquilaTodos.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class HttpClientExtensions
    {
        public static async Task<T> GetObjectAsync<T>(this HttpClient client, string path)
        {
            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.ReadAsObjectAsync<T>();
        }
    }

    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
