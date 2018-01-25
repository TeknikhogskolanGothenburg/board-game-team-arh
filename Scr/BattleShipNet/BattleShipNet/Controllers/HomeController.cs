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

        public HomeController()
        {
            if (games == null)
                games = new GameBoards();
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
            if (Session["GameKey"] != null)
            {
                string gameKey = Session["GameKey"].ToString();
                if (games.DoesItExist(gameKey))
                {
                    GameModel gameModel = new GameModel(games.Get(gameKey));
                    return View(gameModel);
                }
                else
                {
                    Session["GameKey"] = null;
                    Session["PlayerID"] = null;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // Get: Shoot
        public ActionResult Shoot(string x, string y)
        {
            GameModel gameModel = GetSessionGame();
            if (gameModel != null)
            {
                int positionX;
                int positionY;

                if (int.TryParse(x, out positionX) && int.TryParse(y, out positionY))
                {
                    if (positionX <= 10 && positionY <= 10)
                    {
                        Position position = new Position(positionX, positionY);

                        try
                        {
                            gameModel.gameBoard.Shoot(gameModel.EnemyPlayerId, position);
                            Session["PlayerId"] = gameModel.gameBoard.Turn;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, ex);
                        }

                        return RedirectToAction("game", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "You need to hit one existing positions");
                return RedirectToAction("game", "Home");
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
    }
}