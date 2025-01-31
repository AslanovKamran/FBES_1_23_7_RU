using System.Text.Json.Serialization;
using AspNetOmdDb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetOmdDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _domain = @"http://www.omdbapi.com";
        private readonly string _apiKey = @"5df0da60";
        private readonly HttpClient client = new HttpClient();
        public async Task<IActionResult> Index(string title)
        {
            var request = @$"{_domain}/?apiKey={_apiKey}&s={title}";
            var rawJson = await client.GetStringAsync(request);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(rawJson);
            if (apiResult is not null)
            {

                var movies = apiResult.Search;
                var totalCount = apiResult.totalResults;
                ViewBag.Movies = movies;
            }
            return View();
        }

        public async Task<IActionResult> Details(string imdbID)
        {
            var request = @$"{_domain}/?apiKey={_apiKey}&i={imdbID}";
            var rawJson = await client.GetStringAsync(request);

            var movie = JsonConvert.DeserializeObject<Movie>(rawJson);
            if (movie is not null)
            {
                ViewBag.Movie = movie;
            }

            return View();

        }
    }
}
