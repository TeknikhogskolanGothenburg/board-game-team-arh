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
        private static Dictionary<string, List<string>> messages;

        /// <summary>
        /// Deafult constructor
        /// </summary>
        public HomeController()
        {
            if (games == null)
            {
                games = new GameBoards();
                messages = new Dictionary<string, List<string>>();
            }
        }

        /// <summary>
        /// Action for /Home/index.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action for /Home/startgame.cshtml
        /// </summary>
        /// <param name="toDo">Join or new game (string)</param>
        /// <returns>View</returns>
        public ActionResult StartGame(string toDo, string gameKey = null)
        {
            InsertMessages();
            ViewBag.ToDo = toDo;
            ViewBag.GameKey = gameKey;
            return View(games);
        }

        public ActionResult StartNewGame(string name, string privateGame = "")
        {
            if ((name != null) && name.Length >= 2)
            {
                GameBoard gameBoard = new GameBoard();
                games.Add(gameBoard);
                gameBoard.PrivateGame = (privateGame == "true") ? true : false;
                gameBoard.Players[0].Name = name;
                Session["GameKey"] = gameBoard.GameKey;
                Session["PlayerId"] = 0;

                GameModel gameModel = GetSessionGame();


                return RedirectToAction("Waiting", "Home");
            }

            AddMessage("danger", "Your name is too short and it should be mininum 2 characters.");
            return RedirectToAction("StartGame", "Home");
        }

        public ActionResult JoinGame(string name = "", string gameKey = "")
        {
            bool validate = true;

            if ((name == null) || name.Length < 2)
            {
                AddMessage("danger", "Your name is too short and it should be mininum 2 characters.");
                validate = false;
            }

            if((gameKey == null) || gameKey.Length != 6) 
            {
                AddMessage("danger", "Game key is invalid.");
                validate = false;
            }

            if (validate)
            {
                if (games.DoesItExist(gameKey))
                {
                    GameModel gameModel = new GameModel(games.Get(gameKey));

                    gameModel.Game.Players[1].Name = name;
                    Session["GameKey"] = gameKey;
                    Session["PlayerId"] = 1;

                    return RedirectToAction("Game", "Home");
                }
            }
            
            return RedirectToAction("StartGame", "Home", new { todo = "join", gamekey = gameKey });
        }

        /// <summary>
        /// Action for view /Home/waiting.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Waiting()
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                if (!string.IsNullOrEmpty(gameModel.EnemyPlayer.Name))
                {
                    return RedirectToAction("Game", "Home");
                }

                InsertMessages();

                return View(gameModel);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Action for EmailFriend
        /// </summary>
        /*public ActionResult EmailFriend(string email)
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                if (!string.IsNullOrEmpty(gameModel.EnemyPlayer.Name))
                {
                    AddMessage("danger", "Email got never send, for a player had already join this game!");
                    return RedirectToAction("Game", "Home");
                }

                string url = Url.Action("StartGame", "Home", new { todo = "join", gamekey = Session["gamekey"] }, Request.Url.Scheme);

                try
                {
                    MailModel.SendInviteEmail(email, gameModel.YourPlayer.Name, url);

                    AddMessage("success", "A email with game url, was send to your friend!");
                }
                catch (Exception ex)
                {
                    AddMessage("danger", ex.Message);
                }

                return RedirectToAction("Waiting", "Home");
            }

            return RedirectToAction("Index", "Home");
        }*/

<<<<<<< HEAD
        /// <summary>
        /// Action for view /Home/game.cshtml
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Game()
        {
            GameModel gameModel = GetSessionGame();

            if (gameModel != null)
            {
                if (string.IsNullOrEmpty(gameModel.EnemyPlayer.Name))
                {
                    AddMessage("danger", "Game has not started yet, still waiting on opponent player!");
                    return RedirectToAction("Waiting", "Home");
                }
=======
        //public ActionResult JoinGame(string name = "", string gameKey = "")
        //{


        //    if (name != null && name.Length >= 2 && gameKey !=null)
        //    {
        //        GameBoard gameBoard = new GameBoard();
        //        games.Add(gameBoard);
                
        //        gameBoard.Players[0].Name = name;
        //        Session["GameKey"] = gameBoard.GameKey;
        //        Session["PlayerId"] = 0;

        //        GameModel gameModel = GetSessionGame();


        //        return RedirectToAction("JoinSpecificGame", "Home");
        //    }

        //    errors.Add("Your name is too short and it should be mininum 2 characters.");
        //    return RedirectToAction("StartGame", "Home");
        //}
>>>>>>> 2b0ec4c6c8db182130dbe1905f2e614a8d0b046c

                Player winner;
                if (gameModel.Game.IsGameEnd(out winner))
                {
                    return RedirectToAction("GameEnd", "Home");
                }

                InsertMessages();

                return View(gameModel);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Action for view /Home/updategame.cshtml
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
        /// Action for view /Home/shoot.cshtml
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
                    AddMessage("danger", ex.Message);

                    if (json)
                    {
                        InsertMessages();
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
        /// Action for view /Home/gamestat.cshtml (Only to test game code)
        /// </summary>
        /// <returns>View</returns>
        //public ActionResult GameStat()
        //{
        //    GameModel gameModel = GetSessionGame();

        //    if (gameModel != null)
        //    {
        //        return View(gameModel);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        /// <summary>
        /// Action for view /Home/gameend.cshtml
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
        /// Method to add message to messages dictonary under right message type
        /// </summary>
        /// <param name="messageType">Message type (string)</param>
        /// <param name="message">Message (string)</param>
        private void AddMessage(string messageType, string message)
        {
            if (!messages.ContainsKey(messageType))
            {
                messages.Add(messageType, new List<string>());
            }

            messages[messageType].Add(message);
        }

        /// <summary>
        /// Method to help insert messages dictionary to Views
        /// </summary>
        private void InsertMessages()
        {
            ViewBag.Messages = messages;
            messages = new Dictionary<string, List<string>>();
        }
    }
}