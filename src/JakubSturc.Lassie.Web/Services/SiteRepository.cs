using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using JakubSturc.Lassie.Web.Models;
using Microsoft.AspNetCore.Hosting;

namespace JakubSturc.Lassie.Web.Services
{
    public class SiteRepository
    {
        private readonly Lazy<Dictionary<string, Site>> _all;
        private readonly IWebHostEnvironment _environment;

        public SiteRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
            _all = new Lazy<Dictionary<string, Site>>(Load, isThreadSafe: true);
        }

        public IEnumerable<Site> AllSites => _all.Value.Values;

        public Site GetSite(string id) => _all.Value[id];

        private Dictionary<string, Site> Load()
        {
            var path = Path.Combine(_environment.WebRootPath, "data", "sites.json");
            using var reader = File.OpenText(path);
            string json = reader.ReadToEnd(); // todo: reading from file, add async support
            var sites = JsonSerializer.Deserialize<Site[]>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            });
            return sites.ToDictionary(_ => _.Id);
        }
    }
}
