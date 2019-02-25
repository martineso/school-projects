using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
public class AppController : Controller
{
        private IMailService mailService;
        private IConfigurationRoot config;
        private IWorldRepository repository;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        { 
            this.mailService = mailService;
            this.config = config;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Feedback()
        {
            var comments = repository.GetAllComments();
            return View(comments);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(model.Email.Contains("test"))
            {
                ModelState.AddModelError("", "This is an invalid email");
            }

            if(ModelState.IsValid)
            {
                mailService.SendMail(config["MailSettings:ToAddress"], model.Email, "My Portfolio", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent!";
            }

            
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }
    }

}
