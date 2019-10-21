using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.models
{
    public class Request
    {
        public string name { get; set; }
        public string location { get; set; }
        public string avatar_url { get; set; }
        public string repos_url { get; set; }
        public List<Repository> repositories { get; set; }
    }

    public class Repository
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public int stargazers_count { get; set; }
    }
}
