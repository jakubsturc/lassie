﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakubSturc.Lassie.Web.Models;
using JakubSturc.Lassie.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JakubSturc.Lassie.Web.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("list")]
        public async Task<IEnumerable<string>> List()
        {
            return await _searchService.GetSites();
        }

        [HttpGet("{site}/{code}")]
        public async Task<SearchResult> Search([FromRoute] string site, [FromRoute] string code)
        {
            return await _searchService.Search(site, code);
        }
    }
}
