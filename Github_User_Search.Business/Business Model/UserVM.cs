using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Business.Business_Model
{
    public class UserVM
    {

        public string Login { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public List<RepositoryVM> RepositoryList { get; set; }
    }

    public class SearchUserResponseVM
    {
        public string SearchString { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public List<UserVM> UserList { get; set; }

    }
}
