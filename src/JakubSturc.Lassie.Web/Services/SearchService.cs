﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakubSturc.Lassie.Web.Models;

namespace JakubSturc.Lassie.Web.Services
{
    /// <summary>
    /// Encapsulates search logic
    /// </summary>
    public class SearchService
    {
        private readonly SiteRepository _repository;
        private readonly IE11Client _http;

        public SearchService(SiteRepository repository, IE11Client http)
        {
            _repository = repository;
            _http = http;
        }

        /// <summary>
        /// Returns all available sites.
        /// </summary>
        /// <remarks>Method is async just to keep same api as other methods.</remarks>
        public async Task<IEnumerable<string>> GetSites()
        {
            return await Task.FromResult(_repository.AllSites.Select(site => site.Id));
        }

        public async Task<SearchResult> Search(string siteId, string code)
        {
            var site = _repository.GetSite(siteId);
            var url = site.GetSearchUrlFor(code);
            var result = new SearchResult.Builder(site.Id, url);

            try
            {
                var response = site.Method switch
                {
                    "get" => await _http.Get(url),
                    "post" => await _http.Post(url, site.GetFormBodyFor(code)),
                    _ => throw new NotSupportedException()
                };
                                
                return site.HasPossibleMatch(response) ? result.PossibleMatch() : result.NotFound();
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
    }
}
