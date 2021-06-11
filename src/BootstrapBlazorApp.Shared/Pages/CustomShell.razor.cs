using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class CustomShell : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        public string Code { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Code = await GetContent();
            await base.OnInitializedAsync();
        }

        private async Task<string> GetContent()
        {
            var re = await HttpClient.GetStringAsync("/scripts/index.js");
            return re;
        }
    }
}
