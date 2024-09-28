using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class StreamController : Controller
    {
        private readonly HttpClient _httpClient;

        public StreamController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task StreamData()
        {
            string url = "http://192.168.1.102:8080/";
            try
            {
                using (HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        byte[] buffer = new byte[8192];
                        int bytesRead;

                        Response.ContentType = "text/event-stream";

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            string data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            await Response.WriteAsync($"data: {data}\n\n");
                            await Response.Body.FlushAsync();
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Логирование ошибки или другие действия
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                Response.StatusCode = 500; // Internal Server Error
                await Response.WriteAsync("Error: Unable to connect to the video stream.");
            }
            catch (Exception ex)
            {
                // Логирование ошибки или другие действия
                Console.WriteLine($"Exception: {ex.Message}");
                Response.StatusCode = 500; // Internal Server Error
                await Response.WriteAsync("Error: An unexpected error occurred.");
            }
        }
    }
}
