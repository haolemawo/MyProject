using MyProject.Framework.XmlDoc;
using MyProject.Framework.XmlDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class MyProjectBuidlerExtensions
    {
        public static IApplicationBuilder UseXmlConfig(this IApplicationBuilder app,string path)
        {
            XmlHelper.GetXmlDocuments(path);

            return app;
        }
        
    }
}
