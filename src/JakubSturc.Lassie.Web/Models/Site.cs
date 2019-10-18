using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JakubSturc.Lassie.Web.Models
{
    [DebuggerDisplay("{Name}")]
    public class Site
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string SearchUrl { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> NotFoundTexts { get; set; }
        public IEnumerable<string> FoundTexts { get; set; }

        public string GetSearchUrlFor(string code)
        {
            return string.Format(SearchUrl, Uri.EscapeUriString(code));
        }

        public string GetFormBodyFor(string code)
        {
            return string.Format(Body, Uri.EscapeDataString(code));
        }

        public bool HasPossibleMatch(string content)
        {
            if (NotFoundTexts != null)
            {
                return !NotFoundTexts.Any(text => content.Contains(text));
            }

            if (FoundTexts != null)
            {
                return FoundTexts.Any(text => content.Contains(text));
            }

            return true;
        }
    }
}
