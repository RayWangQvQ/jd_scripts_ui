using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class CustomShell : ComponentBase
    {
        public string Code { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Code = await GetContent();
            await base.OnInitializedAsync();
        }

        private async Task<string> GetContent()
        {
            return await Task.FromResult("123\n456");
        }
    }
}
