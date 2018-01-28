using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using GameEngine;
using BattleShipNet.Models;

namespace BattleShipNet.Controllers
{
    public class HomeController : Controller
    {
        private static GameBoards games;
        private static List<string> errors;

        /// <summary>
        /// Deafult constructor
        /// </summary>
        public HomeController()
        {
            if (games == null)
            {
                games = new GameBoards();
                errors = new List<string>();
            }
        }

        /// <summary>
        /// Action for /Home/index.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            // Test code
            GameBoard gameBoard = new GameBoard();
            games.Add(gameBoard);
            gameBoard.PrivateGame = false;
            gameBoard.Players[0].Name = "Hans";
            gameBoard.Players[1].Name = "Greta";
            Session["GameKey"] = gameBoard.GameKey;
            Session["PlayerId"] = 1;
            // Test code end
            return View();
        }

        /// <summary>
        /// Action for /Home/startgame.cshtml
        /// </summary>
        /// <param name="toDo">Join or new game (string)</param>
        /// <returns>View</returns>
        public ActionResult StartGame(string toDo, string gameKey = null)
        {
            ViewBag.ToDo = toDo;
            ViewBag.GameKey = gameKey;
            return View(games);
        }

        /// <summary>
        /// Action for /Home/game.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Game()
        {
            InsertErrors();

            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                Player winner;
                if (gameModel.Game.IsGameEnd(out winner))
                {
                    return RedirectToAction("GameOver", "Home");
                }

                // Test code
                Session["PlayerId"] = gameModel.Game.Turn;

                return View(gameModel);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Action for /Home/updategame.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult UpdateGame()
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                Response.ContentType = "application/json";
                return View(gameModel);
            }

            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }

        /// <summary>
        /// Action for Shoot's
        /// </summary>
        public ActionResult Shoot(string x, string y)
        {
            GameModel gameModel = GetSessionGame();
            bool json = Request.Headers.Get("Accept").StartsWith("application/json");

            if (gameModel != null)
            {
                try
                {
                    bool result = gameModel.Shoot(x, y);

                    ViewBag.Result = result;
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);

                    if (json)
                    {
                        InsertErrors();
                    }
                }

                if (json)
                {
                    Response.ContentType = "application/json";
                    return View(gameModel);
                }
                else {
                    return RedirectToAction("Game", "Home");
                }
            }

            if (json)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return null;
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Action for /Home/gameend.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult GameEnd()
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                Player winner = new Player();
                if (gameModel.Game.IsGameEnd(out winner))
                {
                    ViewBag.Winner = winner;
                    return View(gameModel);
                }
                else
                {
                    return RedirectToAction("Game", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    
        /// <summary>
        /// Takes care get GameBoard from session GameKey
        /// </summary>
        /// <returns>GameModel object with GameBoard</returns>
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

        /// <summary>
        /// Method to help insert error message list to Views
        /// </summary>
        private void InsertErrors()
        {
            ViewBag.Errors = errors;
            errors = new List<string>();
        }
    }
}