using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;
using BattleShipNet.Models;

namespace BattleShipNet.Controllers
{
    public class HomeController : Controller
    {
        private static GameBoards games;
        private static List<Exception> errorsKeeper;

        public HomeController()
        {
            if (games == null)
            {
                games = new GameBoards();
                errorsKeeper = new List<Exception>();
            }  
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.GameKey = games.New();
            Session["GameKey"] = ViewBag.GameKey;
            Session["PlayerId"] = 1;
            return View();
        }

        // GET: StartGame
        public ActionResult StartGame(String toDo)
        {
            ViewBag.ToDo = toDo;
            return View();
        }

        // Get: Game
        public ActionResult Game()
        {
            InsertErrors();

            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                return View(gameModel);
            }

            return RedirectToAction("Index", "Home");
        }

        // Get: Shoot
        public ActionResult Shoot(string x, string y)
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                try
                {
                    gameModel.Shoot(x, y);
                    Session["PlayerId"] = gameModel.gameBoard.Turn;
                }
                catch(Exception ex)
                {
                    errorsKeeper.Add(ex);
                }

                return RedirectToAction("Game", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        private GameModel GetSessionGame()
        {
            if (Session["GameKey"] != null)
            {
                string gameKey = Session["GameKey"].ToString();

                if (games.DoesItExist(gameKey))
                {
                    return new GameModel(games.Get(gameKey));
                }
                else
                {
                    Session["GameKey"] = null;
                    Session["PlayerID"] = null;
                }
            }

            return null;
        }

        private void InsertErrors()
        {
            ViewBag.Errors = errorsKeeper;
            errorsKeeper = new List<Exception>();
        }
    }
}