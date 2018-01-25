using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BattleShipNet.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home
        public ActionResult StartGame(String toDo)
        {
            ViewBag.ToDo = toDo;
            return View();
        }
    }

}