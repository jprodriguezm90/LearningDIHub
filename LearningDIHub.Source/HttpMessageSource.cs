using LearningDIHub.Domain.Models;
using System.Text.Json;

namespace LearningDIHub.DataSource
{
    /*public class HttpMessageSource(IHttpClientFactory httpClientFactory) : IMessageSource
    {

        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("message");*/
    public class HttpMessageSource(HttpClient _httpClient) : IMessageSource
    {
        public Message GetMessage()
        {
            //This is a antipattern, but we are doing it for simplicity. In real world, you should use async/await pattern.
            var stream = _httpClient.GetStreamAsync("message.json").Result;
            Message message = JsonSerializer.Deserialize<Message>(stream);
            message.Id = Guid.NewGuid();
            return message;
        }
    }
}
