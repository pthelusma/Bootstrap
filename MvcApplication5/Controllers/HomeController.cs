using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
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

        public ActionResult Testimonials()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult Plowing()
        {
            return View();
        }

        public ActionResult Landscaping()
        {
            return View();
        }

        public ActionResult Excavation()
        {
            return View();
        }

        public ActionResult Hardscapes()
        {
            return View();
        }

        public ActionResult Septic()
        {
            return View();
        }

        public ActionResult Materials()
        {
            return View();
        }

        public ActionResult Gravel()
        {
            return View();
        }

        public ActionResult Decorative()
        {
            return View();
        }

        public ActionResult Crushed()
        {
            return View();
        }

        public ActionResult Soil()
        {
            return View();
        }

        public ActionResult Mulch()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult SendMail(Communication communication)
        {
            using(var db = new NHExcavationDBEntities())
            {
                communication.CreatedDate = DateTime.Now;
                communication.CreateBy = 0;

                db.Communications.Add(communication);
                db.SaveChanges();
            }

            SendEmail(communication);

            return Json(new { Result = "Success" });
        }

        private void SendConfirmationEmail(Communication communication)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.1and1.com");

            mail.From = new MailAddress("john@nhexcavation.com");
            mail.To.Add(communication.Email);
            mail.Subject = string.Format("Inquiry from {0} {1}", communication.FirstName, communication.LastName);
            mail.Body = String.Format(
                                        "{0} {1}\n" +
                                        "{2}\n" +
                                        "{3}\n" +
                                        "Preferred Contact Method: {4}\n" +
                                        "How did you find us? {5}\n" +
                                        "Materials: {6}\n\n" +
                                        "{7}", communication.FirstName, communication.LastName, communication.PhoneNumber, communication.Email, communication.ContactMethod, communication.DiscoveryMethod, communication.Materials, communication.Message);

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreply@nhexcavation.com", "yamaha1010");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        private void SendEmail(Communication communication)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.1and1.com");

            mail.From = new MailAddress("john@nhexcavation.com");
            mail.To.Add(communication.Email);
            mail.Subject = string.Format("Inquiry from {0} {1}", communication.FirstName, communication.LastName);
            mail.Body = String.Format("{0} {1}\n{2}\n{3}\nPreferred Contact Method: {4}\nHow did you find us? {5}\n\n{6}", communication.FirstName, communication.LastName, communication.PhoneNumber, communication.Email, communication.ContactMethod, communication.DiscoveryMethod, communication.Message);

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreply@nhexcavation.com", "yamaha1010");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
 
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
