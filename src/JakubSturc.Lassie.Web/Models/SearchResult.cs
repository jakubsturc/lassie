using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JakubSturc.Lassie.Web.Models
{
    public class SearchResult
    {
        public string SiteId { get; }
        public string Url { get; }
        public bool WorthChecking { get; }
        public string Error { get; }

        private SearchResult(string siteId, string url, bool worthChecking, string error)
        {
            SiteId = siteId;
            Url = url;
            WorthChecking = worthChecking;
            Error = error;
        }

        public class Builder
        {
            private readonly string _siteId;
            private readonly string _url;

            public Builder(string siteId, string url)
            {
                _siteId = siteId;
                _url = url;
            }

            public SearchResult NotFound() => new SearchResult(_siteId, _url, false, null);
            public SearchResult PossibleMatch() => new SearchResult(_siteId, _url, true, null);
            public SearchResult Error(string msg) => new SearchResult(_siteId, _url, true, msg);
        }
    }
}
