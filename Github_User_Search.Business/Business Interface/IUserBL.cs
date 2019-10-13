using Github_User_Search.Business.Business_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Business.Business_Interface
{
    public interface IUserBL
    {
        Task<ResponseVM<SearchUserResponseVM>> SearchUserAsync(string textToSearch, int page = 1, int itemerPage = 10);
        Task<List<RepositoryVM>> GetTopRatedRepository(string repoOwner);
    }
}
