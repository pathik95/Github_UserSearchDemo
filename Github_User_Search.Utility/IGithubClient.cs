using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Utility
{
    public interface IGithubClient
    {
        Task<T> GetAsync<T>(string apiEndPoint, string accessToken = null) where T : class;
    }
}
