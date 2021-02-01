using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;
using WebCanteen.Areas.Admin.Middleware;

namespace canteenVLU.Areas.Admin.Controllers
{
    [LoginVerification]
    public class ContactController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var contacts = model.CONTACTs.ToList();
            return View(contacts);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contact = model.CONTACTs.FirstOrDefault(x => x.ID == id);
            return View(contact);
        }

        
        [HttpPost]
        public ActionResult Edit(int id,string subject,string message)
        {
            var contact = model.CONTACTs.FirstOrDefault(x => x.ID == id);
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("tuongstudy@gmail.com","Căn tin Văn Lang");
                    var receiverEmail = new MailAddress(contact.EMAIL);
                    var password = "tuong1106";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return RedirectToAction("Index","Contact");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var contact = model.CONTACTs.FirstOrDefault(x => x.ID == id);
            return View(contact);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contact = model.CONTACTs.FirstOrDefault(x => x.ID == id);
            return View(contact);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var contact = model.CONTACTs.FirstOrDefault(x => x.ID == id);
            model.CONTACTs.Remove(contact);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}