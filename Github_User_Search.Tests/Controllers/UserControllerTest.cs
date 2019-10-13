using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Github_User_Search;
using Github_User_Search.Controllers;
using System.Threading.Tasks;
using Github_User_Search.Business.Business_Model;
using Github_User_Search.Utility;

namespace Github_User_Search.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public async Task SearchUserAsync()
        {
            // Arrange
            UserController controller = new UserController();
            string textToSearch = "pathik";
            // Act for Page 1
            ViewResult result = await controller.SearchResult(textToSearch) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(ResponseVM<SearchUserResponseVM>));
            var searchResult = result.Model as ResponseVM<SearchUserResponseVM>;
            Assert.AreEqual(false, searchResult.Error);
            Assert.IsNotNull(searchResult.Data);
            Assert.AreEqual(1, searchResult.Data.CurrentPage);
            Assert.AreEqual(textToSearch, searchResult.Data.SearchString);
            Assert.IsNotNull(searchResult.Data.UserList);
            Assert.AreEqual(10, searchResult.Data.ItemsPerPage);

            Assert.IsFalse(searchResult.Data.UserList.Any(s => string.IsNullOrEmpty(s.Login)));
            Assert.IsFalse(searchResult.Data.UserList.Any(s => s.RepositoryList.Count > 5));



            if (searchResult.Data.TotalPageCount > searchResult.Data.CurrentPage)
            {
                // Act for Page 2
                result = await controller.SearchResult(searchResult.Data.SearchString, searchResult.Data.CurrentPage + 1) as ViewResult;
                Assert.IsInstanceOfType(result.Model, typeof(ResponseVM<SearchUserResponseVM>));
                var searchResultForPage2 = result.Model as ResponseVM<SearchUserResponseVM>;
                Assert.AreEqual(searchResult.Data.CurrentPage + 1, searchResultForPage2.Data.CurrentPage);
                Assert.AreEqual(searchResult.Data.TotalPageCount, searchResultForPage2.Data.TotalPageCount);
                Assert.IsFalse(searchResultForPage2.Data.UserList.Any(s => string.IsNullOrEmpty(s.Login)));
                Assert.IsFalse(searchResultForPage2.Data.UserList.Any(s => s.RepositoryList.Count > 5));
            }

            if (searchResult.Data.TotalPageCount > 2)
            {
                // Act for Last Page
                result = await controller.SearchResult(searchResult.Data.SearchString, searchResult.Data.TotalPageCount) as ViewResult;
                Assert.IsInstanceOfType(result.Model, typeof(ResponseVM<SearchUserResponseVM>));
                var searchResultForLastPage = result.Model as ResponseVM<SearchUserResponseVM>;
                Assert.AreEqual(searchResult.Data.TotalPageCount, searchResultForLastPage.Data.CurrentPage);
                Assert.AreEqual(searchResult.Data.TotalPageCount, searchResultForLastPage.Data.TotalPageCount);
                Assert.IsFalse(searchResultForLastPage.Data.UserList.Any(s => string.IsNullOrEmpty(s.Login)));
                Assert.IsFalse(searchResultForLastPage.Data.UserList.Any(s => s.RepositoryList.Count > 5));
                Assert.IsNotNull(searchResult.Data.UserList);
                Assert.IsTrue(searchResult.Data.UserList.Count > 0);
            }


        }


        [TestMethod]
        public async Task SearchUserZeroResponse()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = await controller.SearchResult("sdfsdsdadadsasadasdadasdad") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(ResponseVM<SearchUserResponseVM>));
            var searchResult = result.Model as ResponseVM<SearchUserResponseVM>;
            Assert.AreEqual(false, searchResult.Error);
            Assert.IsNotNull(searchResult.Data);
            Assert.AreEqual(1, searchResult.Data.CurrentPage);
            Assert.AreEqual("sdfsdsdadadsasadasdadasdad", searchResult.Data.SearchString);
            Assert.IsNotNull(searchResult.Data.UserList);
            Assert.AreEqual(0, searchResult.Data.UserList.Count);
        }


        [TestMethod]
        public async Task SearchUserError()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = await controller.SearchResult(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(ResponseVM<SearchUserResponseVM>));
            var searchResult = result.Model as ResponseVM<SearchUserResponseVM>;
            Assert.AreEqual(true, searchResult.Error);
            Assert.IsNull(searchResult.Data);
            Assert.AreEqual(ErrorMessage.REQUIRED_FIELD_ERROR, searchResult.Message);
        }
    }
}
