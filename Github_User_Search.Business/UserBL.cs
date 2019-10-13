
using Github_User_Search.Business.Business_Interface;
using Github_User_Search.Business.Business_Model;
using Github_User_Search.Utility;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Business
{
    public class UserBL : IUserBL
    {
        private readonly GitHubClient _githubClient;


        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="githubClient"></param>
        public UserBL(string accessToken)
        {
            _githubClient = new GitHubClient(new ProductHeaderValue(Constants.PRODUCT_NAME));
            var tokenAuth = new Credentials(accessToken); // AccessToken from Github
            _githubClient.Credentials = tokenAuth;
        }

        /// <summary>
        /// To search user on using Github API
        /// </summary>
        /// <param name="textToSearch">text to search</param>
        /// <param name="page">current page number</param>
        /// <param name="itemerPage">items per page</param>
        /// <returns></returns>
        public async Task<ResponseVM<SearchUserResponseVM>> SearchUserAsync(string textToSearch, int page = 1, int itemerPage = 10)
        {
            try
            {
                if (string.IsNullOrEmpty(textToSearch))
                {
                    return new ResponseVM<SearchUserResponseVM>(true, ErrorMessage.REQUIRED_FIELD_ERROR);
                }
                var request = new SearchUsersRequest(textToSearch)
                {
                    Page = page,
                    PerPage = itemerPage
                };

                var result = await _githubClient.Search.SearchUsers(request);
                var searchResponseVM = new SearchUserResponseVM()
                {
                    SearchString = textToSearch,
                    CurrentPage = page,
                    ItemsPerPage = itemerPage,
                    TotalPageCount = GetTotalPageCount(result.TotalCount, itemerPage),
                    UserList = result.Items.Select(s => new UserVM()
                    {
                        Id = s.Id,
                        ImageURL = s.AvatarUrl,
                        Location = s.Location,
                        Login = s.Login,
                        Name = s.Name,
                    }).ToList()
                };

                foreach (var user in searchResponseVM.UserList)
                {
                    try
                    {
                        user.RepositoryList = await GetTopRatedRepository(user.Login);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return new ResponseVM<SearchUserResponseVM>(searchResponseVM);
            }
            catch (RateLimitExceededException ex)
            {
                return new ResponseVM<SearchUserResponseVM>(true, ErrorMessage.RATE_LIMIT_ERROR);
            }
            catch (Exception ex)
            {
                return new ResponseVM<SearchUserResponseVM>(true, ErrorMessage.COMMON_ERROR);
            }
        }

        /// <summary>
        /// To get top 5 rated repository of a user
        /// </summary>
        /// <param name="repoOwner"></param>
        /// <returns></returns>
        public async Task<List<RepositoryVM>> GetTopRatedRepository(string repoOwner)
        {
            var result = await _githubClient.Repository.GetAllForUser(repoOwner);

            return result.OrderByDescending(s => s.StargazersCount).Take(5).Select(s => new RepositoryVM()
            {
                Description = s.Description,
                FullName = s.FullName,
                Name = s.Name,
                URL = s.HtmlUrl,
                StargazersCount = s.StargazersCount
            }).ToList();
        }

        private int GetTotalPageCount(int resultCount, int itemPerPage)
        {
            if (itemPerPage > 0)
            {
                var pageCount = (int)Math.Ceiling((decimal)resultCount / (decimal)itemPerPage);
                return pageCount;
            }
            else
                return 0;
        }
    }
}
