using Business.Contracts;
using Business.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class Github : IGithub
    {
        public async Task<string> GetGithubRepos(string SearchQuery)
        {

            var result = new RequestResult<Request>() { Result = new Request() };

            try
            {
                //get userinfo
                var clientRequest = new ClientRequest();
                result = await clientRequest.Get<Request>(Config.UsersUrl.Replace("{username}", SearchQuery));
                if (result.HasError == true)
                {
                    throw new Exception("User is not found.");
                }
                //get repos list
                result.Result.repositories = new List<Repository>();
                var reposList = await clientRequest.Get<List<Repository>>(result.Result.repos_url);
                if (reposList.Result.Count > 5)
                    result.Result.repositories = reposList.Result.OrderByDescending(a => a.stargazers_count).Take(5).ToList();
                else
                    result.Result.repositories = reposList.Result;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ResultMessage = new List<string>() { ex.Message };
            }

            return JsonConvert.SerializeObject(result);

        }
    }
}
