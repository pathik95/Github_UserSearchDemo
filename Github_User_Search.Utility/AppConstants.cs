using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Utility
{
    public class Constants
    {
        public const string QUERY_STRING = "?";
        public const string PRODUCT_NAME = "demo-user-search-app";
        public const string TOKEN_ACCESS_KEY = "GithubToken";
    }

    public class ErrorMessage
    {
        public const string REQUIRED_FIELD_ERROR = "Please enter some text to Search!";
        public const string COMMON_ERROR = "Error executing request! Please try again later.";
        public const string RATE_LIMIT_ERROR = "Daily Limit Reached! Please try again tomorrow.";
    }

    public class MediaType
    {
        public const string JSON = "application/json";
        public const string V3_JSON = "application/vnd.github.v3+json";
    }
    public class BaseUrl
    {
        public const string GithubV3 = @"https://api.github.com";
    }

    public static class EndPoints
    {
        public const string SearchUser = @"/search/users";
    }

}
