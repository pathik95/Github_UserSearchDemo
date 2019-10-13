using Github_User_Search.Business;
using Github_User_Search.Business.Business_Interface;
using Github_User_Search.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Github_User_Search.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL userBL;

        /// <summary>
        /// Contructor
        /// </summary>
        public UserController()
        {
            userBL = new UserBL(ConfigurationManager.AppSettings[Constants.TOKEN_ACCESS_KEY]);
        }

        /// <summary>
        /// Action method to search for the user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To fetch the users list from Github and display it to user 
        /// </summary>
        /// <param name="searchString">user to search on Github</param>
        /// <param name="page">current page number</param>
        /// <returns></returns>
        public async Task<ActionResult> SearchResult(string searchString, int page = 1)
        {
            var searchResult = await userBL.SearchUserAsync(searchString, page);

            return View(searchResult);
        }
    }
}