﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Middleware
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app,
            IApplicationEnvironment env)
        {
            var path = Path.Combine(env.ApplicationBasePath, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = provider;

            app.UseStaticFiles(options);
            return app;
        }
    }
}
