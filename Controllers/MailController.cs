using MailingUsingMailkit.BL.Interface;
using MailingUsingMailkit.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailingUsingMailkit.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailServices mailServices;

        public MailController(IMailServices mailServices)
        {
            this.mailServices = mailServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(MailViewModel model)
        {
            try
            {
                await mailServices.SendAsync(model.To, model.subject, model.Body, model.Attachments);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return View("Index");

            }


        }

        public IActionResult SendWelcomeEmail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendWelcomeEmail(WelcomeMailVM model)
        {
            string welcomeEmail = null;
            var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates/EmailTemplate.html");
            using (var str = new StreamReader(photoPath))
            {
                welcomeEmail = str.ReadToEnd();
            }
            welcomeEmail = welcomeEmail.Replace("[username]", model.EmailTo);


            try
            {
                await mailServices.SendAsync(model.EmailTo, "Welcome Email", welcomeEmail);
                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {

                return View(model);
            }
           
        }
    }
}
