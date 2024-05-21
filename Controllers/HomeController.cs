using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProgramaRafaAnass.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace ProgramaRafaAnass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "4c13866740msh27adf353370af5ep1ab6c5jsn7688b8bb227d");
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "genius-song-lyrics1.p.rapidapi.com");
        }

        public async Task<IActionResult> Index()
        {
            ProgramaRafaAnass.Models.API1.Root chartData = null;

            try
            {
                var requestUri = new Uri("https://genius-song-lyrics1.p.rapidapi.com/chart/albums/?per_page=10&page=1");
                var response = await _client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                chartData = JsonConvert.DeserializeObject<ProgramaRafaAnass.Models.API1.Root>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching data from the API.");
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError(e, "An error occurred while deserializing the JSON response.");
            }

            return View(chartData);
        }

        public async Task<IActionResult> Privacy()
        {
            ProgramaRafaAnass.Models.API2.Root chartData1 = null;

            try
            {
                var requestUri = new Uri("https://genius-song-lyrics1.p.rapidapi.com/chart/artists/?per_page=10&page=1");
                var response = await _client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                chartData1 = JsonConvert.DeserializeObject<ProgramaRafaAnass.Models.API2.Root>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching data from the API.");
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError(e, "An error occurred while deserializing the JSON response.");
            }

            return View(chartData1);
        }

public async Task<IActionResult> GetLyrics(string artist, string songTitle)
{
    if (string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(songTitle))
    {
        ModelState.AddModelError("", "Please provide both artist and song title.");
        return View();
    }

    ProgramaRafaAnass.Models.APILyrics.Root lyricsResult = null;

    try
    {
        var requestUri = new Uri($"https://api.lyrics.ovh/v1/{artist}/{songTitle}");
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        lyricsResult = JsonConvert.DeserializeObject<ProgramaRafaAnass.Models.APILyrics.Root>(responseBody);
    }
    catch (HttpRequestException e)
    {
        _logger.LogError(e, "An error occurred while fetching data from the API.");
    }
    catch (JsonSerializationException e)
    {
        _logger.LogError(e, "An error occurred while deserializing the JSON response.");
    }

    return View(lyricsResult);
}
    }
}
