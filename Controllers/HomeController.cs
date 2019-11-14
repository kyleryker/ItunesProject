using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using iTunesProject.Models;
using System.Threading.Tasks;
using ItunesDataAccess.BusinessLogic;

namespace iTunesProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Music(string term)
        {
            return await Search(term);
        }

        public async Task<ClickCountModel> AddClick(MusicModel music)
        {
            var baseModel = new ClickCountModel();
            if(music.ViewUrl != null)
            {
                baseModel.Url = music.ViewUrl;
            }
            else
            {
                baseModel.Url = music.PreviewUrl;
            }

            return baseModel;
        }

        public async Task<ActionResult> Search(string term)
        {
            string BaseUrl = "http://itunes.apple.com/";
            var result = new MusicListModel();
            var allMusic = new List<MusicModel>();
            var musicWithClicks = new List<MusicWithClicksModel>();
            string searchTerm = ReplaceSpaces(term);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                HttpResponseMessage Res = await client.GetAsync("search?term=" + searchTerm);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<MusicListModel>(response);
                    allMusic = result.Results;
                    foreach (MusicModel item in allMusic)
                    {
                        var fullModel = new MusicWithClicksModel();
                        fullModel.Music = item;
                        fullModel.Clicks = await GetNumClicks(item);
                        musicWithClicks.Add(fullModel);
                    }
                }
                return View(musicWithClicks);
            }          
        }

        public async Task<int> GetNumClicks(MusicModel music)
        {
            if (music.ViewUrl != null) {
                return ClickProcessor.GetClicks(music.ViewUrl);
            }
            else
            {
                return ClickProcessor.GetClicks(music.PreviewUrl);
            }
          
        }

        public ActionResult Clicked(string url)
        {
            ClickProcessor.AddClick(url);

            return Redirect(url);
        }

        static string ReplaceSpaces(string value)
        {
            return Regex.Replace(value, @"\s+", "+");
        }
    }
}