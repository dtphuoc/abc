using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebChat.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        ServiceReference1.Service1Client chats = new ServiceReference1.Service1Client();

        public ActionResult Index()
        {
            if (Session["LoginUser"] != null)
            {
                return View(chats.GetAllChat().ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ServiceReference1.User u)
        {
            if (ModelState.IsValid)
            {
                if (chats.Login(u.UserName, u.Password) == true)
                {
                    //add session
                    Session["LoginUser"] = u.UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ServiceReference1.User u)
        {
            if (ModelState.IsValid)
            {

                if (chats.Register(u.UserName, u.Password).Equals("thanh cong"))
                {
                    Session["LoginUser"] = u.UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Register failed";
                    return View();
                }

            }
            return View();
        }

        public ActionResult SendChat()
        {
            if (Session["LoginUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendChat(ServiceReference1.Chat chat)
        {

            if (chats.SendChat(chat.Content, chat.UserName).Equals("Send chat thanh cong"))
            {
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
