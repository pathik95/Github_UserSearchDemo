using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Business.Business_Model
{
    public class RepositoryVM
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int StargazersCount { get; set; }
    }
}
