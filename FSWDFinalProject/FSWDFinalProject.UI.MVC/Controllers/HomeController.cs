using FSWDFinalProject.UI.MVC.Models;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                //body of the message
                string body = $"{cvm.Name} has sent the following message <br/>" +
                    $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";
                //message object
                MailMessage m = new MailMessage("admin@lexwoodward.com","alexis.woodward1717@outlook.com",cvm.Subject + " - From Job Board", body);
                //allow HTML
                m.IsBodyHtml = true;
                //priority
                m.Priority = MailPriority.High;
                //reply
                m.ReplyToList.Add(cvm.Email);
                
                //configure mail client
                SmtpClient client = new SmtpClient("mail.lexwoodward.ext");
                //config email credentials
                client.Credentials = new NetworkCredential("admin@lexwoodward.com", "NBHD!2020");

                try
                {
                    client.Send(m);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }
                return View("EmailConfirmation", cvm);
            }
            return View(cvm);
        }
    }
}

