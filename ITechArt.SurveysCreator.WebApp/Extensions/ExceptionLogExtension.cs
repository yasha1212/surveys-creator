using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.WebApp.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ITechArt.SurveysCreator.WebApp.Extensions
{
    public static class ExceptionLogExtension
    {
        public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLogMiddleware>();
        }
    }
}
