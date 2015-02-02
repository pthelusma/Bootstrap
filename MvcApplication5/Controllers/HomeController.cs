using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Submit(string clientip, string challenge, string response)
        {

            HttpClient client = new HttpClient();

            string privatekey = "6LcXkfUSAAAAAGQO8dXYce09FzVPyMRoCK2ONzHo";

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("remoteip", HttpUtility.UrlEncode(clientip)));
            values.Add(new KeyValuePair<string, string>("privatekey", HttpUtility.UrlEncode(privatekey)));
            values.Add(new KeyValuePair<string, string>("challenge", HttpUtility.UrlEncode(challenge)));
            values.Add(new KeyValuePair<string, string>("response", HttpUtility.UrlEncode(response)));

            var content = new FormUrlEncodedContent(values);
            var postResponse = client.PostAsync("http://www.google.com/recaptcha/api/verify", content).Result;

            string resultContent = postResponse.Content.ReadAsStringAsync().Result;

            string[] responseResults = resultContent.Split(new string[] { "\n", "\\n" }, StringSplitOptions.RemoveEmptyEntries);

            Response resp = new Response()
            {
                result = responseResults[0],
                code = responseResults[1]
            };

            return Json(resp);
        }

        public class Response
        {
            public string result { get; set; }
            public string code { get; set; }
        }
    }
}
