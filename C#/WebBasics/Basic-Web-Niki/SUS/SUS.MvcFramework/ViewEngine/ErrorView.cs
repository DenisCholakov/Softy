using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.ViewEngine
{
    public class ErrorView : IView
    {
        private readonly IEnumerable<string> _errors;
        private readonly string csharpCode;

        public ErrorView(IEnumerable<string> errors, string csharpCode)
        {
            this._errors = errors;
            this.csharpCode = csharpCode;
        }
        public string GetHtml(object viewModel)
        {
            var html = new StringBuilder();

            html.AppendLine($"<h1>View compile {this._errors.Count()} errors:</h1>");

            foreach (var error in this._errors)
            {
                html.AppendLine($"<li>{error}</li>");
            }

            html.AppendLine($"<ul><pre>{csharpCode}</pre>");

            return html.ToString();
        }
    }
}
