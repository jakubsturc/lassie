using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JakubSturc.Lassie.Web.Models;
using Microsoft.AspNetCore.Hosting;

namespace JakubSturc.Lassie.Web.Services
{
    public class SiteRepository
    {
        private readonly Task<Dictionary<string, Site>> _load;
        private readonly IWebHostEnvironment _environment;

        public SiteRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
            _load = Task.Run(Load);
        }

        public async Task<IEnumerable<Site>> GetAllSites() => (await _load).Values;
        
        public async Task<Site > GetSite(string id) => (await _load)[id];

        private Dictionary<string, Site> Load()
        {
            var path = Path.Combine(_environment.WebRootPath, "data", "sites.json");
            using var reader = File.OpenText(path);
            string json = reader.ReadToEnd();
            var sites = JsonSerializer.Deserialize<Site[]>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            });
            return sites.ToDictionary(_ => _.Id);
        }
    }
}
